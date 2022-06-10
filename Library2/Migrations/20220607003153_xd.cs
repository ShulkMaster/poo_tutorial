using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library2.Migrations
{
    public partial class xd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Xd",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Xd",
                table: "Roles");
        }
    }
}
