using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCore.Data.Migrations
{
    public partial class initializeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    AttributeName = table.Column<string>(maxLength: 50, nullable: true),
                    AttributeValue = table.Column<string>(maxLength: 50, nullable: true),
                    AttributeType = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    GetUrl = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    Url = table.Column<string>(type: "varchar(200)", maxLength: 100, nullable: false, comment: "说明"),
                    FamilyName = table.Column<string>(maxLength: 10, nullable: true),
                    LastName = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true, computedColumnSql: "[FamilyName]+[LastName]"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MxQuestionCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<long>(nullable: false),
                    ParentName = table.Column<string>(maxLength: 50, nullable: true),
                    Hierarchy = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MxQuestionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MxQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Question = table.Column<string>(maxLength: 500, nullable: true),
                    QuestionType = table.Column<string>(maxLength: 50, nullable: false),
                    QuestionCate = table.Column<string>(maxLength: 50, nullable: true),
                    Answer = table.Column<string>(maxLength: 500, nullable: true),
                    Options = table.Column<string>(maxLength: 50, nullable: true),
                    Tags = table.Column<string>(maxLength: 100, nullable: true),
                    Mx_QuestionCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MxQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MxQuestions_MxQuestionCategories_Mx_QuestionCategoryId",
                        column: x => x.Mx_QuestionCategoryId,
                        principalTable: "MxQuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MxQuestions_Mx_QuestionCategoryId",
                table: "MxQuestions",
                column: "Mx_QuestionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseAttributes");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "MxQuestions");

            migrationBuilder.DropTable(
                name: "MxQuestionCategories");
        }
    }
}
