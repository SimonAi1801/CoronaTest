using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaTest.Persistence.Migrations
{
    public partial class _initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Door = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobilphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postalcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialSecurityNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postalcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlotCapacity = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignTestCenter",
                columns: table => new
                {
                    AvailableCampaignsId = table.Column<int>(type: "int", nullable: false),
                    AvailableTestCentersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignTestCenter", x => new { x.AvailableCampaignsId, x.AvailableTestCentersId });
                    table.ForeignKey(
                        name: "FK_CampaignTestCenter_Campaigns_AvailableCampaignsId",
                        column: x => x.AvailableCampaignsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignTestCenter_TestCenters_AvailableTestCentersId",
                        column: x => x.AvailableTestCentersId,
                        principalTable: "TestCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    ParticipantId = table.Column<int>(type: "int", nullable: true),
                    TestCenterId = table.Column<int>(type: "int", nullable: true),
                    TestResult = table.Column<int>(type: "int", nullable: false),
                    ExaminationState = table.Column<int>(type: "int", nullable: false),
                    ExaminationAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_TestCenters_TestCenterId",
                        column: x => x.TestCenterId,
                        principalTable: "TestCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignTestCenter_AvailableTestCentersId",
                table: "CampaignTestCenter",
                column: "AvailableTestCentersId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CampaignId",
                table: "Examinations",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ParticipantId",
                table: "Examinations",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_TestCenterId",
                table: "Examinations",
                column: "TestCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignTestCenter");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "TestCenters");
        }
    }
}
