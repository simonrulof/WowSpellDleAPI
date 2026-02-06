using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WowSpellDleAPI.Models;

public class Spell
{
    public Spell()
    {
    }

    public Spell(int _id,
                 SpellGenericColumn _name,
                 SpellGenericColumn _description,
                 SpellGenericColumn _class,
                 SpellGenericColumn _school,
                 SpellGenericColumn _useType, 
                 int _cooldown, 
                 string _iconPath,
                 List<SpellGenericColumn> _specs)
    {
        Id = _id;
        Name = _name;
        Description = _description;
        Class = _class;
        School = _school;
        UseType = _useType;
        Cooldown = _cooldown;
        IconPath = _iconPath;
        Specs = _specs;
    }

    [Required]
    public int Id { get; set; }
    
    [Required]
    public int NameId { get; set; }
    public SpellGenericColumn Name { get; set; } = null!;

    [Required]
    public int DescriptionId { get; set; }
    public SpellGenericColumn Description { get; set; } = null!;

    [Required]
    public int ClassId { get; set; }
    public SpellGenericColumn Class { get; set; } = null!;

    [Required]
    public int SchoolId { get; set; }
    public SpellGenericColumn School { get; set; } = null!;

    [Required]
    public int UseTypeId { get; set; }
    public SpellGenericColumn UseType { get; set; } = null!;

    [Required]
    public int Cooldown { get; set; }
    
    [MaxLength(500)]
    public string IconPath { get; set; } = string.Empty;

    [Required]
    public List<SpellGenericColumn> Specs { get; set; } = new();
}