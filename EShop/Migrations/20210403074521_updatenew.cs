using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Migrations
{
    public partial class updatenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "AspNetUsers",
                newName: "Name");
        }
    }
}
