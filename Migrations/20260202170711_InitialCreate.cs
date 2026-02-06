using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WowSpellDleAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpellCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fr = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    En = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpellGenericColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fr = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    En = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellGenericColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellGenericColumns_SpellCategories_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "SpellCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameId = table.Column<int>(type: "integer", nullable: false),
                    DescriptionId = table.Column<int>(type: "integer", nullable: false),
                    ClassId = table.Column<int>(type: "integer", nullable: false),
                    SchoolId = table.Column<int>(type: "integer", nullable: false),
                    UseTypeId = table.Column<int>(type: "integer", nullable: false),
                    Cooldown = table.Column<int>(type: "integer", nullable: false),
                    IconPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spells_SpellGenericColumns_ClassId",
                        column: x => x.ClassId,
                        principalTable: "SpellGenericColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spells_SpellGenericColumns_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "SpellGenericColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spells_SpellGenericColumns_NameId",
                        column: x => x.NameId,
                        principalTable: "SpellGenericColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spells_SpellGenericColumns_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SpellGenericColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spells_SpellGenericColumns_UseTypeId",
                        column: x => x.UseTypeId,
                        principalTable: "SpellGenericColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpellSpecMapping",
                columns: table => new
                {
                    SpellId = table.Column<int>(type: "integer", nullable: false),
                    SpecId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSpecMapping", x => new { x.SpellId, x.SpecId });
                    table.ForeignKey(
                        name: "FK_SpellSpecMapping_SpellGenericColumns_SpecId",
                        column: x => x.SpecId,
                        principalTable: "SpellGenericColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellSpecMapping_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpellCategories_En",
                table: "SpellCategories",
                column: "En");

            migrationBuilder.CreateIndex(
                name: "IX_SpellCategories_Fr",
                table: "SpellCategories",
                column: "Fr");

            migrationBuilder.CreateIndex(
                name: "IX_SpellGenericColumns_ColumnId",
                table: "SpellGenericColumns",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellGenericColumns_En",
                table: "SpellGenericColumns",
                column: "En");

            migrationBuilder.CreateIndex(
                name: "IX_SpellGenericColumns_Fr",
                table: "SpellGenericColumns",
                column: "Fr");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_ClassId",
                table: "Spells",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_DescriptionId",
                table: "Spells",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_NameId",
                table: "Spells",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_SchoolId",
                table: "Spells",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_UseTypeId",
                table: "Spells",
                column: "UseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellSpecMapping_SpecId",
                table: "SpellSpecMapping",
                column: "SpecId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpellSpecMapping");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "SpellGenericColumns");

            migrationBuilder.DropTable(
                name: "SpellCategories");
        }
    }
}
