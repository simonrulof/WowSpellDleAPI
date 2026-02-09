using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using WowSpellDleAPI.Data;
using WowSpellDleAPI.DTO;
using WowSpellDleAPI.Enum;
using WowSpellDleAPI.Models;

namespace WowSpellDleAPI.Logic
{
    public class SpellLogic
    {

        private readonly ApplicationDbContext _context;
        public SpellLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SpellModel> GetAllSpells()
        {
            var spellList = new List<SpellModel>();
            
            var spells = _context.Spells
                .Include(s => s.Name)
                .Include( s => s.School)
                .Include(s => s.UseType)
                .Include(s => s.Class)
                .ToList();

            foreach (var spell in spells)
            {
                var id = spell.Id;
                var translations = new Translations(CreateTranslationFields("En", spell),
                                                    CreateTranslationFields("Fr", spell));
                var cooldown = spell.Cooldown;
                var iconPath = spell.IconPath;

                var spellModel = new SpellModel(id, translations, cooldown, iconPath);
                spellList.Add(spellModel);
            }
            return spellList;
        }

        public TranslationFields CreateTranslationFields(string lang, Spell spell)
        {
            string name = spell.Name.GetByLanguage(lang);
            string _class = spell.Class.GetByLanguage(lang);
            List<string> specs = new List<string>();
            foreach (var spec in spell.Specs)
            {
                specs.Add(spec.GetByLanguage(lang));
            }
            string school = spell.School.GetByLanguage(lang);
            string useType = spell.UseType.GetByLanguage(lang);

            return new TranslationFields(name, _class, specs, school, useType);
        }
        
        private Spell GenerateDailySpell()
        {
            DateTime today = DateTime.Now;
            if (_context.DailySpells.Any(ds => ds.Date == DateOnly.FromDateTime(today)))
            {
                throw new Exception("Daily spell has already been generated.");
            }
            bool identToPreviouses;
            Random rand = new Random();
            Spell spell;
            do
            {
                identToPreviouses = false;
                int toSkip = rand.Next(0, _context.DailySpells.Count());
                spell = _context.Spells.Skip(toSkip).Take(1)
                .Include(s => s.Name)
                .Include(s => s.School)
                .Include(s => s.UseType)
                .Include(s => s.Class)
                .First();
                
                for (int i = 1; i < 10; i++) // check the 10 previous spells
                {
                    DateOnly date = DateOnly.FromDateTime(today.AddDays(-1 * i));
                    var previousDailySpell = _context.DailySpells.Where(ds => ds.Date == date);
                    if (previousDailySpell.Any() && previousDailySpell.First().Spell.Id == spell.Id)
                    {
                        identToPreviouses = true;
                        continue;
                    }
                }
            } while(identToPreviouses);

            _context.DailySpells.Add(new DailySpell(DateOnly.FromDateTime(today), spell.Id));
            _context.SaveChanges();

            return spell;
        }

        private Spell GetDailySpell(DateTime date)
        {
            var dailySpell = _context.DailySpells.Where(ds => ds.Date == DateOnly.FromDateTime(date));
            if (dailySpell.Any())
            {
                return dailySpell
                    .Include(ds => ds.Spell)
                    .Include(ds => ds.Spell.Name)
                    .Include(ds => ds.Spell.School)
                    .Include(ds => ds.Spell.UseType)
                    .Include(ds => ds.Spell.Class)
                    .First().Spell;
            }
            if (DateOnly.FromDateTime(date) != DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot generate daily spell for other days than today.");
            }
            return GenerateDailySpell();

        }   

        public GuessAnswerModel GuessAnswer(int Id, DateTime date)
        {
            var model = new GuessAnswerModel();
            var spells = _context.Spells.Where(s => s.Id == Id);
            if (!spells.Any())
                throw new Exception("guess id not correct.");

            var guessedSpell = spells
                .Include(s => s.Name)
                .Include(s => s.School)
                .Include(s => s.UseType)
                .Include(s => s.Class)
                .First();
            var dailySpell = GetDailySpell(date);


            model.Spell = guessedSpell.Id == dailySpell.Id ? GuessFieldAnswer.Correct : GuessFieldAnswer.Incorrect;
            model.Class = guessedSpell.Class.Id == dailySpell.Class.Id ? GuessFieldAnswer.Correct : GuessFieldAnswer.Incorrect;
            
            var inter = guessedSpell.Specs.Intersect(dailySpell.Specs);
            if (inter.Count() == 0)
            {
                model.Spec = GuessFieldAnswer.Incorrect;
            }
            if (inter.Count() == dailySpell.Specs.Count() 
                && guessedSpell.Specs.Count() == dailySpell.Specs.Count()) {
                model.Spec = GuessFieldAnswer.Correct;
            }
            else
            {
                model.Spec = GuessFieldAnswer.PartiallyCorrect;
            }

            model.School = guessedSpell.School.Id == dailySpell.School.Id ? GuessFieldAnswer.Correct : GuessFieldAnswer.Incorrect;
            model.UseType = guessedSpell.UseType.Id == dailySpell.UseType.Id ? GuessFieldAnswer.Correct : GuessFieldAnswer.Incorrect;
            
            if (guessedSpell.Cooldown == dailySpell.Cooldown)
            {
                model.Cooldown = GuessFieldAnswerRelative.Correct;
            }
            else if (guessedSpell.Cooldown < dailySpell.Cooldown)
            {
                model.Cooldown = GuessFieldAnswerRelative.More;
            }
            else
            {
                model.Cooldown = GuessFieldAnswerRelative.Less;
            }
            return model;
        }
    }
}
