using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class Endpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateGoOut",
                table: "WorkRegisterEvents",
                newName: "DateGoOut");

            migrationBuilder.RenameColumn(
                name: "dateGoIn",
                table: "WorkRegisterEvents",
                newName: "DateGoIn");

            migrationBuilder.AddColumn<int>(
                name: "EndpointInID",
                table: "WorkRegisterEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndpointOutID",
                table: "WorkRegisterEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RegisterTimeEndpoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    EndpointType = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterTimeEndpoint", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkRegisterEvents_EndpointInID",
                table: "WorkRegisterEvents",
                column: "EndpointInID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRegisterEvents_EndpointOutID",
                table: "WorkRegisterEvents",
                column: "EndpointOutID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTimeEndpoint_Name",
                table: "RegisterTimeEndpoint",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRegisterEvents_RegisterTimeEndpoint_EndpointInID",
                table: "WorkRegisterEvents",
                column: "EndpointInID",
                principalTable: "RegisterTimeEndpoint",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRegisterEvents_RegisterTimeEndpoint_EndpointOutID",
                table: "WorkRegisterEvents",
                column: "EndpointOutID",
                principalTable: "RegisterTimeEndpoint",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkRegisterEvents_RegisterTimeEndpoint_EndpointInID",
                table: "WorkRegisterEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRegisterEvents_RegisterTimeEndpoint_EndpointOutID",
                table: "WorkRegisterEvents");

            migrationBuilder.DropTable(
                name: "RegisterTimeEndpoint");

            migrationBuilder.DropIndex(
                name: "IX_WorkRegisterEvents_EndpointInID",
                table: "WorkRegisterEvents");

            migrationBuilder.DropIndex(
                name: "IX_WorkRegisterEvents_EndpointOutID",
                table: "WorkRegisterEvents");

            migrationBuilder.DropColumn(
                name: "EndpointInID",
                table: "WorkRegisterEvents");

            migrationBuilder.DropColumn(
                name: "EndpointOutID",
                table: "WorkRegisterEvents");

            migrationBuilder.RenameColumn(
                name: "DateGoOut",
                table: "WorkRegisterEvents",
                newName: "dateGoOut");

            migrationBuilder.RenameColumn(
                name: "DateGoIn",
                table: "WorkRegisterEvents",
                newName: "dateGoIn");
        }
    }
}
