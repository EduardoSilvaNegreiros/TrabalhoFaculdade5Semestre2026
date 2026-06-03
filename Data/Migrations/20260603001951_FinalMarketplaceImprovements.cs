using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalMarketplaceImprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusModeracao",
                table: "Produtos",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "Aprovado");

            migrationBuilder.CreateTable(
                name: "CarrinhoPersistidoItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiraEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoPersistidoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoPersistidoItens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_StatusModeracao",
                table: "Produtos",
                column: "StatusModeracao");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoPersistidoItens_ExpiraEm",
                table: "CarrinhoPersistidoItens",
                column: "ExpiraEm");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoPersistidoItens_ProdutoId",
                table: "CarrinhoPersistidoItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoPersistidoItens_UsuarioEmail_ProdutoId",
                table: "CarrinhoPersistidoItens",
                columns: new[] { "UsuarioEmail", "ProdutoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoPersistidoItens");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_StatusModeracao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "StatusModeracao",
                table: "Produtos");
        }
    }
}
