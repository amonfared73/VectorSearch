using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VectorSearch.EF.Migrations
{
    /// <inheritdoc />
    public partial class dict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_DictionaryType_DictionaryTypeId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "DictionaryTypeId",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_DictionaryType_DictionaryTypeId",
                table: "Words",
                column: "DictionaryTypeId",
                principalTable: "DictionaryType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_DictionaryType_DictionaryTypeId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "DictionaryTypeId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_DictionaryType_DictionaryTypeId",
                table: "Words",
                column: "DictionaryTypeId",
                principalTable: "DictionaryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
