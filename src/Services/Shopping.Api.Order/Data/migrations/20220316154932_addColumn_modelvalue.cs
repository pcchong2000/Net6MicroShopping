using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Api.Order.Data.migrations
{
    public partial class addColumn_modelvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductModelId",
                table: "OrderItem",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProductModelValue",
                table: "OrderItem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductModelValue",
                table: "OrderItem");
        }
    }
}
