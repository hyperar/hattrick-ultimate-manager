using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyperar.HUM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DevLeagueInactiveFlagBytes : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InactiveFlagBytes",
                table: "League");
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "InactiveFlagBytes",
                table: "League",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0])
                .Annotation("Relational:ColumnOrder", 24);
        }
    }
}