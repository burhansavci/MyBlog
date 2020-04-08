using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.DataAccess.Migrations
{
    public partial class Added_Unique_Constraint_To_Language : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "language_code_unique",
                table: "Languages",
                column: "language_code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "language_code_unique",
                table: "Languages");
        }
    }
}
