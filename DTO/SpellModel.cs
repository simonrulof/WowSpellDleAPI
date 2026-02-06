using WowSpellDleAPI.Enum;

namespace WowSpellDleAPI.DTO;


public class SpellModel
{
    public SpellModel (int _id, Translations _translations, int _cooldown, string _iconPath)
    {
        Id = _id;
        Translations = _translations;
        Cooldown = _cooldown;
        IconPath = _iconPath;
    }


    public int Id { get; set; }
    public Translations Translations { get; set; }
    public int Cooldown { get; set; }
    public string IconPath { get; set; }
}

public class Translations
{
    public Translations(TranslationFields _en, TranslationFields _fr)
    {
        En = _en;
        Fr = _fr;
    }

    public TranslationFields En { get; set; }
    public TranslationFields Fr { get; set; }

    // Méthode helper
    public TranslationFields GetByLanguage(string lang) => lang switch
    {
        "En" => En,
        "Fr" => Fr,
        _ => throw new ArgumentException($"Langue non supportée: {lang}")
    };
}

public class TranslationFields
{
    public TranslationFields (string _name, string _description, string _class, List<string> _spec, string _school, string _useType)
    {
        Name = _name;
        Description = _description;
        Class = _class;
        Spec = _spec;
        School = _school;
        UseType = _useType;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Class { get; set; }
    public List<string> Spec { get; set; }
    public string School { get; set; }
    public string UseType { get; set; }
}
