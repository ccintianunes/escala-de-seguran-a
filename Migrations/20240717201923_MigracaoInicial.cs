using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscalaSegurancaAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escalas",
                columns: table => new
                {
                    EscalaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraSaida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalas", x => x.EscalaId);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    LocalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.LocalId);
                });

            migrationBuilder.CreateTable(
                name: "Policiais",
                columns: table => new
                {
                    PolicialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policiais", x => x.PolicialId);
                });

            migrationBuilder.CreateTable(
                name: "Marcacoes",
                columns: table => new
                {
                    MarcacaoEscalaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicialId = table.Column<int>(type: "int", nullable: false),
                    EscalaId = table.Column<int>(type: "int", nullable: false),
                    LocalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcacoes", x => x.MarcacaoEscalaId);
                    table.ForeignKey(
                        name: "FK_Marcacoes_Escalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escalas",
                        principalColumn: "EscalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marcacoes_Locais_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Locais",
                        principalColumn: "LocalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marcacoes_Policiais_PolicialId",
                        column: x => x.PolicialId,
                        principalTable: "Policiais",
                        principalColumn: "PolicialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_EscalaId",
                table: "Marcacoes",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_LocalId",
                table: "Marcacoes",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_PolicialId",
                table: "Marcacoes",
                column: "PolicialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marcacoes");

            migrationBuilder.DropTable(
                name: "Escalas");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "Policiais");
        }
    }
}
