using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260531143000_AddProdutoSlug")]
    public partial class AddProdutoSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Produtos",
                type: "TEXT",
                maxLength: 140,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE Produtos SET Slug = 'produto-' || Id WHERE Slug = '' OR Slug IS NULL;");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Slug",
                table: "Produtos",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_Slug",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Produtos");
        }
    }
}
