using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AddedAssetLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_AppUserId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PricePerOZ",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceSPOT",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    SourceAssetId = table.Column<int>(type: "int", nullable: false),
                    LikedAssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.SourceAssetId, x.LikedAssetId });
                    table.ForeignKey(
                        name: "FK_Likes_Assets_LikedAssetId",
                        column: x => x.LikedAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Assets_SourceAssetId",
                        column: x => x.SourceAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikedAssetId",
                table: "Likes",
                column: "LikedAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_AppUserId",
                table: "Assets",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_AppUserId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PricePerOZ",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PriceSPOT",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Assets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_AppUserId",
                table: "Assets",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
