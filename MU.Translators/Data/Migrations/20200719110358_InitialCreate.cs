using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MU.Translators.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<string>(nullable: false),
                    PublishedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranslatorGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslatorGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    TranslatorRegistered = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    ApprovedByAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    DateApprovedByAdmin = table.Column<DateTime>(nullable: true),
                    TranslatorGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translators_TranslatorGroups_TranslatorGroupId",
                        column: x => x.TranslatorGroupId,
                        principalTable: "TranslatorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(nullable: false),
                    TranslatorId = table.Column<int>(nullable: false),
                    TranslatorGroupId = table.Column<int>(nullable: false),
                    DateTranslated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    Visible = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translations_TranslatorGroups_TranslatorGroupId",
                        column: x => x.TranslatorGroupId,
                        principalTable: "TranslatorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translations_Translators_TranslatorId",
                        column: x => x.TranslatorId,
                        principalTable: "Translators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_TitleId",
                table: "Translations",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_TranslatorGroupId",
                table: "Translations",
                column: "TranslatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_TranslatorId",
                table: "Translations",
                column: "TranslatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Translators_TranslatorGroupId",
                table: "Translators",
                column: "TranslatorGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Translators");

            migrationBuilder.DropTable(
                name: "TranslatorGroups");
        }
    }
}
