using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Migrations
{
    public partial class AddColumnRoleInProfileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Profiles");
        }
    }
}
