using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WowSpellDleAPI.Models;

public class SpellGenericColumn
{
    public SpellGenericColumn()
    {
    }

    public SpellGenericColumn(int _id, string _fr, string _en, SpellCategory _column)
    {
        Id = _id;
        Fr = _fr;
        En = _en;
        Column = _column;
    }

    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Fr { get; set; } = string.Empty;

    [Required]
    public string En { get; set; } = string.Empty;

    [Required]
    public int ColumnId { get; set; }
    public SpellCategory Column { get; set; } = null!;

    // Méthode helper
    public string GetByLanguage(string lang) => lang switch
    {
        "En" => En,
        "Fr" => Fr,
        _ => throw new ArgumentException($"Langue non supportée: {lang}")
    };
}