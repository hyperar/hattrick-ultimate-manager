#nullable disable

namespace Hyperar.HUM.Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class TeamDetails : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeniorTeam");
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeniorTeam",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FoundedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamIndex = table.Column<int>(type: "int", nullable: false),
                    IsPlayingCup = table.Column<bool>(type: "bit", nullable: false),
                    GlobalPowerRating = table.Column<int>(type: "int", nullable: false),
                    LeaguePowerRating = table.Column<int>(type: "int", nullable: false),
                    RegionPowerRating = table.Column<int>(type: "int", nullable: false),
                    PowerRating = table.Column<int>(type: "int", nullable: false),
                    LeagueRank = table.Column<int>(type: "int", nullable: false),
                    UndefeatedStreak = table.Column<int>(type: "int", nullable: false),
                    WinningStreak = table.Column<int>(type: "int", nullable: false),
                    CanBookMidweekFriendly = table.Column<bool>(type: "bit", nullable: false),
                    CanBookWeekendFriendly = table.Column<bool>(type: "bit", nullable: false),
                    LogoBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HomeMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AwayMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorTeam", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_SeniorTeam_League",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_SeniorTeam_Manager",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_SeniorTeam_Region",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeam_LeagueId",
                table: "SeniorTeam",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeam_ManagerId",
                table: "SeniorTeam",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeam_RegionId",
                table: "SeniorTeam",
                column: "RegionId");
        }
    }
}