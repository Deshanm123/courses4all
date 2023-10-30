using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses4All.Data.Migrations
{
    public partial class Minorchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_Categories_UserCategoryId",
                table: "UserCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserCategories_UserCategoryId",
                table: "UserCategories");

            migrationBuilder.DropColumn(
                name: "UserCategoryId",
                table: "UserCategories");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_CategoryId",
                table: "UserCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_Categories_CategoryId",
                table: "UserCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_Categories_CategoryId",
                table: "UserCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserCategories_CategoryId",
                table: "UserCategories");

            migrationBuilder.AddColumn<int>(
                name: "UserCategoryId",
                table: "UserCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserCategoryId",
                table: "UserCategories",
                column: "UserCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_Categories_UserCategoryId",
                table: "UserCategories",
                column: "UserCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
