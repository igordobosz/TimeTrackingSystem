using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackingSystem.Data.Migrations
{
    public partial class EnumHandling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndpointType",
                table: "RegisterTimeEndpoint",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndpointType",
                table: "RegisterTimeEndpoint",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
