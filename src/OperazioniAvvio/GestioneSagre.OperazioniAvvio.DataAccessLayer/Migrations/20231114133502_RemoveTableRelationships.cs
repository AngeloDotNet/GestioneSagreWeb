using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impostazioni_Feste_IdFesta",
                table: "Impostazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Intestazioni_Feste_IdFesta",
                table: "Intestazioni");

            migrationBuilder.DropIndex(
                name: "IX_Intestazioni_IdFesta",
                table: "Intestazioni");

            migrationBuilder.DropIndex(
                name: "IX_Impostazioni_IdFesta",
                table: "Impostazioni");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Intestazioni_IdFesta",
                table: "Intestazioni",
                column: "IdFesta");

            migrationBuilder.CreateIndex(
                name: "IX_Impostazioni_IdFesta",
                table: "Impostazioni",
                column: "IdFesta");

            migrationBuilder.AddForeignKey(
                name: "FK_Impostazioni_Feste_IdFesta",
                table: "Impostazioni",
                column: "IdFesta",
                principalTable: "Feste",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Intestazioni_Feste_IdFesta",
                table: "Intestazioni",
                column: "IdFesta",
                principalTable: "Feste",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
