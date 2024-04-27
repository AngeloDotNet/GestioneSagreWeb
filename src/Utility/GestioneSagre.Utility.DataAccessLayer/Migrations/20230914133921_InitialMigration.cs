using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestioneSagre.Utility.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatoScontrino",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valore = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatoScontrino", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valore = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valore = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoScontrino",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valore = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoScontrino", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StatoScontrino",
                columns: new[] { "Id", "Valore" },
                values: new object[,]
                {
                    { new Guid("0a4bfb8a-7e75-46fe-a8c1-8d1eb3011686"), "ANNULLATO" },
                    { new Guid("5ac9505a-726a-4106-89f8-a86639558d2d"), "DA INCASSARE" },
                    { new Guid("80087fd8-09bb-45a5-a4b7-afb6007ed592"), "PAGATO" },
                    { new Guid("828e7daa-f1cb-45eb-981a-983fe5cad00b"), "CHIUSO" },
                    { new Guid("bec5963a-9a76-4610-a951-2e3e9e66fd82"), "APERTO" },
                    { new Guid("dced34e3-0be1-4b04-9895-4816e9642644"), "IN ELABORAZIONE" }
                });

            migrationBuilder.InsertData(
                table: "TipoCliente",
                columns: new[] { "Id", "Valore" },
                values: new object[,]
                {
                    { new Guid("0c86400f-4dee-4354-99b2-f680cc6e8a74"), "STAFF" },
                    { new Guid("69ad8e35-b445-42a4-b3c2-8c3fb0a27750"), "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "TipoPagamento",
                columns: new[] { "Id", "Valore" },
                values: new object[] { new Guid("6340fa14-271f-4565-ace2-3249e5f474ef"), "CONTANTI" });

            migrationBuilder.InsertData(
                table: "TipoScontrino",
                columns: new[] { "Id", "Valore" },
                values: new object[,]
                {
                    { new Guid("4981bcd3-6860-4545-8786-832abdbbe60d"), "OMAGGIO" },
                    { new Guid("6e00a736-d5b4-4494-aa89-c2caa726d4e3"), "PAGAMENTO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatoScontrino");

            migrationBuilder.DropTable(
                name: "TipoCliente");

            migrationBuilder.DropTable(
                name: "TipoPagamento");

            migrationBuilder.DropTable(
                name: "TipoScontrino");
        }
    }
}
