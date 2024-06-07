using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VectorSearch.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class embed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Embeddings",
                table: "Words",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Embeddings",
                table: "Words");
        }
    }
}
