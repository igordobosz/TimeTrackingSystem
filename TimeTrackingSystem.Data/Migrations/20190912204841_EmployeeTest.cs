using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class EmployeeTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeGroups_EmployeeGroupID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeGroups_EmployeeGroupID1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRegisterEvents_Employees_EmployeeID1",
                table: "WorkRegisterEvents");

            migrationBuilder.DropIndex(
                name: "IX_WorkRegisterEvents_EmployeeID1",
                table: "WorkRegisterEvents");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeGroupID1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeID1",
                table: "WorkRegisterEvents");

            migrationBuilder.DropColumn(
                name: "EmployeeGroupID1",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeGroups_EmployeeGroupID",
                table: "Employees",
                column: "EmployeeGroupID",
                principalTable: "EmployeeGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeGroups_EmployeeGroupID",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID1",
                table: "WorkRegisterEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeGroupID1",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkRegisterEvents_EmployeeID1",
                table: "WorkRegisterEvents",
                column: "EmployeeID1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeGroupID1",
                table: "Employees",
                column: "EmployeeGroupID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeGroups_EmployeeGroupID",
                table: "Employees",
                column: "EmployeeGroupID",
                principalTable: "EmployeeGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeGroups_EmployeeGroupID1",
                table: "Employees",
                column: "EmployeeGroupID1",
                principalTable: "EmployeeGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRegisterEvents_Employees_EmployeeID1",
                table: "WorkRegisterEvents",
                column: "EmployeeID1",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
