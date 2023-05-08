using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteApi.Migrations
{
    /// <inheritdoc />
    public partial class addapiimgur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "Cover",
                table: "Mangas",
                newName: "CoverUrl");

            migrationBuilder.AlterColumn<string>(
                name: "PageUrl",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverUrl",
                table: "Mangas",
                newName: "Cover");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PageUrl",
                table: "Pages",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverUrl",
                table: "Pages",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
