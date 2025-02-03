using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class MakePictureUrlNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentText",
                table: "Comments",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "CommentText");

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
