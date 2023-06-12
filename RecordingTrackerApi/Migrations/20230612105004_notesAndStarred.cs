using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordingTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class notesAndStarred : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Starred",
                table: "Instruments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Starred",
                table: "Instruments");
        }
    }
}
