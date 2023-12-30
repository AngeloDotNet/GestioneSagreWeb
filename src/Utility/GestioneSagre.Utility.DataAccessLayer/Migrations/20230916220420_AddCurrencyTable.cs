using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestioneSagre.Utility.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("0a4bfb8a-7e75-46fe-a8c1-8d1eb3011686"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("5ac9505a-726a-4106-89f8-a86639558d2d"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("80087fd8-09bb-45a5-a4b7-afb6007ed592"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("828e7daa-f1cb-45eb-981a-983fe5cad00b"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("bec5963a-9a76-4610-a951-2e3e9e66fd82"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("dced34e3-0be1-4b04-9895-4816e9642644"));

            migrationBuilder.DeleteData(
                table: "TipoCliente",
                keyColumn: "Id",
                keyValue: new Guid("0c86400f-4dee-4354-99b2-f680cc6e8a74"));

            migrationBuilder.DeleteData(
                table: "TipoCliente",
                keyColumn: "Id",
                keyValue: new Guid("69ad8e35-b445-42a4-b3c2-8c3fb0a27750"));

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: new Guid("6340fa14-271f-4565-ace2-3249e5f474ef"));

            migrationBuilder.DeleteData(
                table: "TipoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("4981bcd3-6860-4545-8786-832abdbbe60d"));

            migrationBuilder.DeleteData(
                table: "TipoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("6e00a736-d5b4-4494-aa89-c2caa726d4e3"));

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valore = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Simbolo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Descrizione", "Simbolo", "Valore" },
                values: new object[,]
                {
                    { new Guid("1062f8a7-07ae-4ff3-9122-e1061344e968"), "Sterlina UK", "£", "GBP" },
                    { new Guid("39b42616-38bf-4ca8-ac47-e7ab5415247e"), "Euro", "€", "EUR" },
                    { new Guid("4cb5ba1f-0a26-4904-9335-a4e7a6df957b"), "Dollaro USA", "$", "USD" }
                });

            migrationBuilder.InsertData(
                table: "StatoScontrino",
                columns: new[] { "Id", "Valore" },
                values: new object[,]
                {
                    { new Guid("3cc4bff5-0e3a-48e5-bc0a-a6518b4f9227"), "ANNULLATO" },
                    { new Guid("7a449f0c-773a-4e1b-b195-48765c93b64a"), "PAGATO" },
                    { new Guid("ada6fe1c-1cff-41fa-88d5-adde721ccb6e"), "DA INCASSARE" },
                    { new Guid("ba8d1d47-2980-4081-adfa-59a8a283c168"), "APERTO" },
                    { new Guid("cd7435a1-8352-451e-9640-4b55bcec0d3f"), "CHIUSO" },
                    { new Guid("d3bca587-1d4b-478f-a1cf-77283cacad54"), "IN ELABORAZIONE" }
                });

            migrationBuilder.InsertData(
                table: "TipoCliente",
                columns: new[] { "Id", "Valore" },
                values: new object[,]
                {
                    { new Guid("21a3647c-ebf6-4b3e-afd5-054ee7e53343"), "STAFF" },
                    { new Guid("b8fbba5b-8395-42be-bafb-c1bdffc141c0"), "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "TipoPagamento",
                columns: new[] { "Id", "Valore" },
                values: new object[] { new Guid("f88348a3-6cbb-4108-a79e-e38bc07824d2"), "CONTANTI" });

            migrationBuilder.InsertData(
                table: "TipoScontrino",
                columns: new[] { "Id", "Valore" },
                values: new object[,]
                {
                    { new Guid("4fd0dba1-d49e-4d33-8ce2-fc35bb607141"), "OMAGGIO" },
                    { new Guid("c1b77737-f730-4220-a556-92ca67203779"), "PAGAMENTO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("3cc4bff5-0e3a-48e5-bc0a-a6518b4f9227"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("7a449f0c-773a-4e1b-b195-48765c93b64a"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("ada6fe1c-1cff-41fa-88d5-adde721ccb6e"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("ba8d1d47-2980-4081-adfa-59a8a283c168"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("cd7435a1-8352-451e-9640-4b55bcec0d3f"));

            migrationBuilder.DeleteData(
                table: "StatoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("d3bca587-1d4b-478f-a1cf-77283cacad54"));

            migrationBuilder.DeleteData(
                table: "TipoCliente",
                keyColumn: "Id",
                keyValue: new Guid("21a3647c-ebf6-4b3e-afd5-054ee7e53343"));

            migrationBuilder.DeleteData(
                table: "TipoCliente",
                keyColumn: "Id",
                keyValue: new Guid("b8fbba5b-8395-42be-bafb-c1bdffc141c0"));

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: new Guid("f88348a3-6cbb-4108-a79e-e38bc07824d2"));

            migrationBuilder.DeleteData(
                table: "TipoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("4fd0dba1-d49e-4d33-8ce2-fc35bb607141"));

            migrationBuilder.DeleteData(
                table: "TipoScontrino",
                keyColumn: "Id",
                keyValue: new Guid("c1b77737-f730-4220-a556-92ca67203779"));

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
    }
}
