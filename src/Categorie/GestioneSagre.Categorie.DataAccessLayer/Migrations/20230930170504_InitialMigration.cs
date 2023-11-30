using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneSagre.Categorie.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFesta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaVideo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CategoriaStampa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}
