using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAdvance.DAL.Migrations
{
    public partial class updateTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Float",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVarChar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "PPPP");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "NVarChar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NText",
                oldMaxLength: 100,
                oldDefaultValue: "XXXX");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "Float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "NVarChar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "PPPP",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "NText",
                maxLength: 100,
                nullable: false,
                defaultValue: "XXXX",
                oldClrType: typeof(string),
                oldType: "NVarChar");
        }
    }
}
