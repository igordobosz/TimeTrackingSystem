using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class EmployeeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdentityCode",
                table: "Employees",
                column: "IdentityCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_IdentityCode",
                table: "Employees");
        }
    }
}
