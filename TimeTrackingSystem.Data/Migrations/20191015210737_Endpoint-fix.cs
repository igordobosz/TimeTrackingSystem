using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class Endpointfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EndpointOutID",
                table: "WorkRegisterEvents",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EndpointOutID",
                table: "WorkRegisterEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
