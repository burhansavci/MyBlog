using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.DataAccess.Migrations
{
    public partial class Initial_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    language_code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    is_default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("language_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("article_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password_hash = table.Column<byte[]>(nullable: false),
                    password_salt = table.Column<byte[]>(nullable: false),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category_Translations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 1024, nullable: true),
                    category_id = table.Column<int>(nullable: false),
                    language_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_translation_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "category_translation_category_id_fkey",
                        column: x => x.category_id,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "category_translation_language_id_fkey",
                        column: x => x.language_id,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publish_date = table.Column<DateTime>(nullable: false),
                    view_count = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "article_category_id_fkey",
                        column: x => x.category_id,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "article_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Article_Translations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 100, nullable: false),
                    content_summary = table.Column<string>(maxLength: 1024, nullable: false),
                    content_main = table.Column<string>(nullable: false),
                    article_id = table.Column<int>(nullable: false),
                    language_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("article_translation_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "article_translation_article_id_fkey",
                        column: x => x.article_id,
                        principalSchema: "dbo",
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "article_translation_language_id_fkey",
                        column: x => x.language_id,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    content_main = table.Column<string>(nullable: false),
                    publish_date = table.Column<DateTime>(nullable: false),
                    article_id = table.Column<int>(nullable: false),
                    parent_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comment_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "comment_article_id_fkey",
                        column: x => x.article_id,
                        principalSchema: "dbo",
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "comment_parent_id_fkey",
                        column: x => x.parent_id,
                        principalSchema: "dbo",
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_main = table.Column<bool>(nullable: false),
                    url = table.Column<string>(nullable: false),
                    public_id = table.Column<string>(maxLength: 255, nullable: false),
                    article_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("picture_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "picture_article_id_fkey",
                        column: x => x.article_id,
                        principalSchema: "dbo",
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "article_translation_ix_article_id",
                schema: "dbo",
                table: "Article_Translations",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "article_translation_ix_language_id",
                schema: "dbo",
                table: "Article_Translations",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "article_ix_category_id",
                schema: "dbo",
                table: "Articles",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "article_ix_user_id",
                schema: "dbo",
                table: "Articles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "category_translation_ix_category_id",
                schema: "dbo",
                table: "Category_Translations",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "category_translation_ix_language_id",
                schema: "dbo",
                table: "Category_Translations",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "comment_ix_article_id",
                schema: "dbo",
                table: "Comments",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "comment_ix_parent_id",
                schema: "dbo",
                table: "Comments",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "picture_ix_article_id",
                schema: "dbo",
                table: "Pictures",
                column: "article_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article_Translations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Category_Translations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pictures",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Articles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
