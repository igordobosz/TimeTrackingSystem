using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class FixEmployeeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmployeeGroups",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "EmployeeGroups",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
