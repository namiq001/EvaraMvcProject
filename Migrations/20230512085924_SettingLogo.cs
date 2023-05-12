using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaraMVC.Migrations
{
    public partial class SettingLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoName",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoName",
                table: "Settings");
        }
    }
}
