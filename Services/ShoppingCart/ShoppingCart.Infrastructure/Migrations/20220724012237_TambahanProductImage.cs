using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Infrastructure.Migrations
{
    public partial class TambahanProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "CartItems");
        }
    }
}
