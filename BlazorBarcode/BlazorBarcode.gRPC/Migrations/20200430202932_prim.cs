using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBarcode.gRPC.Migrations
{
    public partial class prim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BarcodeNo",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "BarcodeNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "BarcodeNo",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
