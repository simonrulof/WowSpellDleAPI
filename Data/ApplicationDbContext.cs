using Microsoft.EntityFrameworkCore;
using WowSpellDleAPI.Models;

namespace WowSpellDleAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Spell> Spells { get; set; }
    public DbSet<SpellGenericColumn> SpellGenericColumns { get; set; }
    public DbSet<SpellCategory> SpellCategories { get; set; }
    public DbSet<DailySpell> DailySpells { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure SpellCategory entity
        modelBuilder.Entity<SpellCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Auto-generate ID
            entity.Property(e => e.Fr).IsRequired().HasMaxLength(200);
            entity.Property(e => e.En).IsRequired().HasMaxLength(200);
            
            // Index for better query performance
            entity.HasIndex(e => e.Fr);
            entity.HasIndex(e => e.En);
        });

        // Configure SpellGenericColumn entity
        modelBuilder.Entity<SpellGenericColumn>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Auto-generate ID
            entity.Property(e => e.Fr).IsRequired().HasMaxLength(500);
            entity.Property(e => e.En).IsRequired().HasMaxLength(500);
            
            // Foreign key relationship to SpellCategory
            entity.HasOne(e => e.Column)
                .WithMany()
                .HasForeignKey(e => e.ColumnId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Index for better query performance
            entity.HasIndex(e => e.ColumnId);
            entity.HasIndex(e => e.Fr);
            entity.HasIndex(e => e.En);
        });

        // Configure Spell entity
        modelBuilder.Entity<Spell>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever(); // Manual ID assignment
            entity.Property(e => e.IconPath).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Cooldown).IsRequired();
            
            // Foreign key relationship to Name (SpellGenericColumn)
            entity.HasOne(e => e.Name)
                .WithMany()
                .HasForeignKey(e => e.NameId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Foreign key relationship to Description (SpellGenericColumn)
            entity.HasOne(e => e.Description)
                .WithMany()
                .HasForeignKey(e => e.DescriptionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Foreign key relationship to Class (SpellGenericColumn)
            entity.HasOne(e => e.Class)
                .WithMany()
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Foreign key relationship to School (SpellGenericColumn)
            entity.HasOne(e => e.School)
                .WithMany()
                .HasForeignKey(e => e.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Foreign key relationship to UseType (SpellGenericColumn)
            entity.HasOne(e => e.UseType)
                .WithMany()
                .HasForeignKey(e => e.UseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationship with Specs (SpellGenericColumn)
            entity.HasMany(e => e.Specs)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "SpellSpecMapping",
                    j => j.HasOne<SpellGenericColumn>()
                        .WithMany()
                        .HasForeignKey("SpecId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Spell>()
                        .WithMany()
                        .HasForeignKey("SpellId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("SpellId", "SpecId");
                        j.ToTable("SpellSpecMapping");
                    });
            
            // Indexes for foreign keys
            entity.HasIndex(e => e.NameId);
            entity.HasIndex(e => e.DescriptionId);
            entity.HasIndex(e => e.ClassId);
            entity.HasIndex(e => e.SchoolId);
            entity.HasIndex(e => e.UseTypeId);
        });
        // Configure DailySpell entity
        modelBuilder.Entity<DailySpell>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Auto-generate ID
            entity.Property(e => e.Date).IsRequired();

            // Foreign key relationship to DailySpell
            entity.HasOne(e => e.Spell)
                .WithMany()
                .HasForeignKey(e => e.SpellId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index for better query performance
            entity.HasIndex(e => e.SpellId);
            entity.HasIndex(e => e.Date);
        });
    }
}
