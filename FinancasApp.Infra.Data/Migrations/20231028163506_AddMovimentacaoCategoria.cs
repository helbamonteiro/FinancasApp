using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancasApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovimentacaoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_PERFIL_PERFILID",
                table: "USUARIO");

            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MOVIMENTACAO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DATA = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TIPO = table.Column<int>(type: "int", nullable: false),
                    CATEGORIAID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USUARIOID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIMENTACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOVIMENTACAO_CATEGORIA_CATEGORIAID",
                        column: x => x.CATEGORIAID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOVIMENTACAO_USUARIO_USUARIOID",
                        column: x => x.USUARIOID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_NOME",
                table: "CATEGORIA",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMENTACAO_CATEGORIAID",
                table: "MOVIMENTACAO",
                column: "CATEGORIAID");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMENTACAO_USUARIOID",
                table: "MOVIMENTACAO",
                column: "USUARIOID");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_PERFIL_PERFILID",
                table: "USUARIO",
                column: "PERFILID",
                principalTable: "PERFIL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_PERFIL_PERFILID",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "MOVIMENTACAO");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_PERFIL_PERFILID",
                table: "USUARIO",
                column: "PERFILID",
                principalTable: "PERFIL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
