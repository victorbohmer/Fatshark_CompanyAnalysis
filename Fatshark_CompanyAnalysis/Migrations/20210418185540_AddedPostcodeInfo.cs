using Microsoft.EntityFrameworkCore.Migrations;

namespace Fatshark_CompanyAnalysis.Migrations
{
    public partial class AddedPostcodeInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostcodeInfos",
                columns: table => new
                {
                    PostcodeInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    postcode = table.Column<string>(type: "TEXT", nullable: true),
                    eastings = table.Column<int>(type: "INTEGER", nullable: false),
                    northings = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostcodeInfos", x => x.PostcodeInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostcodeInfos");
        }
    }
}
