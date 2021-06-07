using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioDb.Migrations
{
    public partial class removeBlogLinkProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogLink",
                table: "Blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogLink",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
