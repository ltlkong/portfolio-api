using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioDb.Migrations
{
    public partial class AddLastViewedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastViewed",
                table: "Views",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastViewed",
                table: "Views");
        }
    }
}
