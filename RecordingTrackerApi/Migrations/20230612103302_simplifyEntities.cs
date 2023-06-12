using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordingTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class simplifyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ParentId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Songs_ParentId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_ParentId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Songs",
                newName: "AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ParentId",
                table: "Songs",
                newName: "IX_Songs_AlbumId");

            migrationBuilder.RenameColumn(
                name: "completion",
                table: "Parts",
                newName: "Completion");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Parts",
                newName: "SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_ParentId",
                table: "Parts",
                newName: "IX_Parts_SongId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Albums",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_ParentId",
                table: "Albums",
                newName: "IX_Albums_ArtistId");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Starred",
                table: "Songs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Starred",
                table: "Parts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Starred",
                table: "Artists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Starred",
                table: "Albums",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Songs_SongId",
                table: "Parts",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Songs_SongId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Starred",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Starred",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Starred",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Starred",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "Songs",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                newName: "IX_Songs_ParentId");

            migrationBuilder.RenameColumn(
                name: "Completion",
                table: "Parts",
                newName: "completion");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "Parts",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_SongId",
                table: "Parts",
                newName: "IX_Parts_ParentId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Albums",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums",
                newName: "IX_Albums_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ParentId",
                table: "Albums",
                column: "ParentId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Songs_ParentId",
                table: "Parts",
                column: "ParentId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_ParentId",
                table: "Songs",
                column: "ParentId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
