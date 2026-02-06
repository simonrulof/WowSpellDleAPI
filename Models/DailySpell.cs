using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WowSpellDleAPI.Models;

public class DailySpell
{
    public DailySpell()
    {
    }

    public DailySpell(DateOnly _date, int _spellId)
    {
        Date = _date;
        SpellId = _spellId;
    }

    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public int SpellId { get; set; }
    public Spell Spell { get; set; } = null!;
}
