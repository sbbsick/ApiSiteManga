using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace TesteApi.Migrations
{
    /// <inheritdoc />
    public partial class testemangacover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CoverUrl",
                table: "Pages",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Mangas");
        }
    }
}
