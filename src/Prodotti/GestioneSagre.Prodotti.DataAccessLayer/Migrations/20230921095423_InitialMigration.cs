using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneSagre.Prodotti.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFesta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProdottoAttivo = table.Column<bool>(type: "bit", nullable: false),
                    IdCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prezzo_Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    QuantitaAttiva = table.Column<bool>(type: "bit", nullable: false),
                    QuantitaScorta = table.Column<int>(type: "int", nullable: false),
                    AvvisoScorta = table.Column<bool>(type: "bit", nullable: false),
                    Prenotazione = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prodotti");
        }
    }
}
