using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WowSpellDleAPI.Models;

public class SpellCategory
{
    public SpellCategory()
    {
    }

    public SpellCategory(int _id, string _fr, string _en)
    {
        Id = _id;
        Fr = _fr;
        En = _en;
    }

    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Fr { get; set; } = string.Empty;

    [Required]
    public string En { get; set; } = string.Empty;
}