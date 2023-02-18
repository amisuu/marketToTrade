using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AmendingLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Assets_SourceAssetId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "SourceAssetId",
                table: "Likes",
                newName: "SourceUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_SourceUserId",
                table: "Likes",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_SourceUserId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "SourceUserId",
                table: "Likes",
                newName: "SourceAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Assets_SourceAssetId",
                table: "Likes",
                column: "SourceAssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
