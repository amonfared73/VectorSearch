using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VectorSearch.EF.Migrations
{
    /// <inheritdoc />
    public partial class dic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DictionaryTypeId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DictionaryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_DictionaryTypeId",
                table: "Words",
                column: "DictionaryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_DictionaryType_DictionaryTypeId",
                table: "Words",
                column: "DictionaryTypeId",
                principalTable: "DictionaryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_DictionaryType_DictionaryTypeId",
                table: "Words");

            migrationBuilder.DropTable(
                name: "DictionaryType");

            migrationBuilder.DropIndex(
                name: "IX_Words_DictionaryTypeId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "DictionaryTypeId",
                table: "Words");
        }
    }
}
