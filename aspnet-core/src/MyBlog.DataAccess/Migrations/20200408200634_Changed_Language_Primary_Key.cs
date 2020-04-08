using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.DataAccess.Migrations
{
    public partial class Changed_Language_Primary_Key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "article_translation_language_id_fkey",
                schema: "dbo",
                table: "Article_Translations");

            migrationBuilder.DropForeignKey(
                name: "category_translation_language_id_fkey",
                schema: "dbo",
                table: "Category_Translations");

            migrationBuilder.DropIndex(
                name: "category_translation_ix_language_id",
                schema: "dbo",
                table: "Category_Translations");

            migrationBuilder.DropIndex(
                name: "article_translation_ix_language_id",
                schema: "dbo",
                table: "Article_Translations");

            migrationBuilder.DropPrimaryKey(
                name: "language_pkey",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "language_code_unique",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "language_id",
                schema: "dbo",
                table: "Category_Translations");

            migrationBuilder.DropColumn(
                name: "language_id",
                schema: "dbo",
                table: "Article_Translations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "language_code",
                schema: "dbo",
                table: "Category_Translations",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "language_code",
                schema: "dbo",
                table: "Article_Translations",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "language_pkey",
                table: "Languages",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "category_translation_ix_language_code",
                schema: "dbo",
                table: "Category_Translations",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "article_translation_ix_language_code",
                schema: "dbo",
                table: "Article_Translations",
                column: "language_code");

            migrationBuilder.AddForeignKey(
                name: "article_translation_language_code_fkey",
                schema: "dbo",
                table: "Article_Translations",
                column: "language_code",
                principalTable: "Languages",
                principalColumn: "language_code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_translation_language_code_fkey",
                schema: "dbo",
                table: "Category_Translations",
                column: "language_code",
                principalTable: "Languages",
                principalColumn: "language_code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "article_translation_language_code_fkey",
                schema: "dbo",
                table: "Article_Translations");

            migrationBuilder.DropForeignKey(
                name: "category_translation_language_code_fkey",
                schema: "dbo",
                table: "Category_Translations");

            migrationBuilder.DropIndex(
                name: "category_translation_ix_language_code",
                schema: "dbo",
                table: "Category_Translations");

            migrationBuilder.DropIndex(
                name: "article_translation_ix_language_code",
                schema: "dbo",
                table: "Article_Translations");

            migrationBuilder.DropPrimaryKey(
                name: "language_pkey",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "language_code",
                schema: "dbo",
                table: "Category_Translations");

            migrationBuilder.DropColumn(
                name: "language_code",
                schema: "dbo",
                table: "Article_Translations");

            migrationBuilder.AddColumn<int>(
                name: "language_id",
                schema: "dbo",
                table: "Category_Translations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "language_id",
                schema: "dbo",
                table: "Article_Translations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "language_pkey",
                table: "Languages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "category_translation_ix_language_id",
                schema: "dbo",
                table: "Category_Translations",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "article_translation_ix_language_id",
                schema: "dbo",
                table: "Article_Translations",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "language_code_unique",
                table: "Languages",
                column: "language_code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "article_translation_language_id_fkey",
                schema: "dbo",
                table: "Article_Translations",
                column: "language_id",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_translation_language_id_fkey",
                schema: "dbo",
                table: "Category_Translations",
                column: "language_id",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
