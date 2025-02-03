using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class addedContentManagemtEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "BlogPosts",
                newName: "PictureUrl");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "BlogPosts",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BlogPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "PortfolioItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItems_CategoryId",
                table: "PortfolioItems",
                column: "CategoryId");
        }
    }
}
