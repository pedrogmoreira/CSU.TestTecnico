using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSU.EtapaTecnica.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NOTA_FISCAL",
                columns: table => new
                {
                    CODNOTA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODVENDA = table.Column<int>(nullable: true),
                    DESTINATARIOREMETENTE = table.Column<string>(maxLength: 100, nullable: true),
                    DTEMISSAO = table.Column<DateTime>(nullable: true),
                    DTSAIDAENTRADA = table.Column<DateTime>(nullable: true),
                    NUMNOTA = table.Column<int>(nullable: true),
                    VALORTOTALPRODUTOS = table.Column<double>(nullable: true),
                    VALORTOTALNOTA = table.Column<double>(nullable: true),
                    TRANSFRETE = table.Column<int>(nullable: true),
                    NUMERORECIBO = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTA_FISCAL", x => x.CODNOTA);
                });

            migrationBuilder.CreateTable(
                name: "NOTAFISCALITENS",
                columns: table => new
                {
                    CODITEM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODNOTA = table.Column<int>(nullable: false),
                    CODPRO = table.Column<int>(nullable: false),
                    DESCRPRO = table.Column<string>(maxLength: 80, nullable: true),
                    UNIDADE = table.Column<string>(maxLength: 3, nullable: true),
                    QTDE = table.Column<double>(nullable: false),
                    VALORTOTAL = table.Column<double>(nullable: false),
                    CODIGOPRODUTOEXTERNO = table.Column<string>(maxLength: 20, nullable: true),
                    VALORUNITARIO = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTAFISCALITENS", x => x.CODITEM);
                    table.ForeignKey(
                        name: "FK_NOTAFISCALITENS_NOTAFISCAL",
                        column: x => x.CODNOTA,
                        principalTable: "NOTA_FISCAL",
                        principalColumn: "CODNOTA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NOTAFISCALITENS_CODNOTA",
                table: "NOTAFISCALITENS",
                column: "CODNOTA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTAFISCALITENS");

            migrationBuilder.DropTable(
                name: "NOTA_FISCAL");
        }
    }
}
