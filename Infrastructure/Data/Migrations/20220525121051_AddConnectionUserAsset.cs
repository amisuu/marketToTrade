using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AddConnectionUserAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AppUserId",
                table: "Assets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_AppUserId",
                table: "Assets",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_AppUserId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AppUserId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");
        }
    }
}
