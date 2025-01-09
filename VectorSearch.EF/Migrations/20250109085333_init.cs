using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VectorSearch.EF.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "DictionaryTypes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DictionaryTypes", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Words",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Vector = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DictionaryTypeId = table.Column<int>(type: "int", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Words", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Words_DictionaryTypes_DictionaryTypeId",
            //            column: x => x.DictionaryTypeId,
            //            principalTable: "DictionaryTypes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Words_DictionaryTypeId",
                table: "Words",
                column: "DictionaryTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "DictionaryTypes");
        }
    }
}
