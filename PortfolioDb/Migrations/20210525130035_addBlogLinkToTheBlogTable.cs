using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioDb.Migrations
{
    public partial class addBlogLinkToTheBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Blogs",
                newName: "BlogLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogLink",
                table: "Blogs",
                newName: "Content");
        }
    }
}
