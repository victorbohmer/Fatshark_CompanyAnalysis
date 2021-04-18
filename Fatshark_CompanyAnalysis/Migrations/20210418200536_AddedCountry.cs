using Microsoft.EntityFrameworkCore.Migrations;

namespace Fatshark_CompanyAnalysis.Migrations
{
    public partial class AddedCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "PostcodeInfos",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country",
                table: "PostcodeInfos");
        }
    }
}
