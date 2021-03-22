using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogs.Infrastructure.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlDesc",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HtmlDesc",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
