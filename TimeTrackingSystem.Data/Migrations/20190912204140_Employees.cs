using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class Employees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeGroups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false),
                    WorkingHoursPerWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeGroupID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surename = table.Column<string>(nullable: false),
                    IdentityCode = table.Column<string>(nullable: true),
                    EmployeeGroupID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeGroups_EmployeeGroupID",
                        column: x => x.EmployeeGroupID,
                        principalTable: "EmployeeGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeGroups_EmployeeGroupID1",
                        column: x => x.EmployeeGroupID1,
                        principalTable: "EmployeeGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkRegisterEvents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    dateGoIn = table.Column<DateTime>(nullable: false),
                    dateGoOut = table.Column<DateTime>(nullable: false),
                    EmployeeID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRegisterEvents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkRegisterEvents_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkRegisterEvents_Employees_EmployeeID1",
                        column: x => x.EmployeeID1,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeGroupID",
                table: "Employees",
                column: "EmployeeGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeGroupID1",
                table: "Employees",
                column: "EmployeeGroupID1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRegisterEvents_EmployeeID",
                table: "WorkRegisterEvents",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRegisterEvents_EmployeeID1",
                table: "WorkRegisterEvents",
                column: "EmployeeID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkRegisterEvents");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeGroups");
        }
    }
}
