#nullable disable

namespace Hyperar.HUM.Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class DevSeniorPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeniorPlayerSkillSet");

            migrationBuilder.DropTable(
                name: "SeniorPlayer");
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeniorPlayer",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AgeYears = table.Column<int>(type: "int", nullable: false),
                    AgeDays = table.Column<int>(type: "int", nullable: false),
                    ShirtNumber = table.Column<int>(type: "int", nullable: true),
                    JoinedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    HasMotherClubBonus = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Statement = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalSkillIndex = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Agreeability = table.Column<int>(type: "int", nullable: false),
                    Aggressiveness = table.Column<int>(type: "int", nullable: false),
                    Honesty = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<int>(type: "int", nullable: false),
                    Loyalty = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    HealthStatus = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Form = table.Column<int>(type: "int", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    JuniorNationalTeamMatches = table.Column<int>(type: "int", nullable: false),
                    SeniorNationalTeamMatches = table.Column<int>(type: "int", nullable: false),
                    TeamMatches = table.Column<int>(type: "int", nullable: false),
                    TeamGoals = table.Column<int>(type: "int", nullable: false),
                    TeamAssists = table.Column<int>(type: "int", nullable: false),
                    SeasonSeriesGoals = table.Column<int>(type: "int", nullable: false),
                    SeasonCupGoals = table.Column<int>(type: "int", nullable: false),
                    SeasonFriendlyGoals = table.Column<int>(type: "int", nullable: false),
                    CareerGoals = table.Column<int>(type: "int", nullable: false),
                    CareerHattricks = table.Column<int>(type: "int", nullable: false),
                    CareerAssists = table.Column<int>(type: "int", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SeniorTeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorPlayer", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_SeniorPlayer_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_SeniorPlayer_SeniorTeam",
                        column: x => x.SeniorTeamHattrickId,
                        principalTable: "SeniorTeam",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "SeniorPlayerSkillSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Keeper = table.Column<int>(type: "int", nullable: false),
                    Defender = table.Column<int>(type: "int", nullable: false),
                    Playmaking = table.Column<int>(type: "int", nullable: false),
                    Winger = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Scoring = table.Column<int>(type: "int", nullable: false),
                    SetPieces = table.Column<int>(type: "int", nullable: false),
                    SeniorPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorPlayerSkillSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeniorPlayerSkillSet_SeniorPlayer",
                        column: x => x.SeniorPlayerHattrickId,
                        principalTable: "SeniorPlayer",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeniorPlayer_CountryHattrickId",
                table: "SeniorPlayer",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorPlayer_SeniorTeamHattrickId",
                table: "SeniorPlayer",
                column: "SeniorTeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorPlayerSkillSet_SeniorPlayerHattrickId",
                table: "SeniorPlayerSkillSet",
                column: "SeniorPlayerHattrickId");
        }
    }
}