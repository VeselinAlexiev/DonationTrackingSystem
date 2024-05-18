using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationTrackingSystem.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignCreators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCreators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignCreators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignCreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GoalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_CampaignCreators_CampaignCreatorId",
                        column: x => x.CampaignCreatorId,
                        principalTable: "CampaignCreators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_AspNetUsers_DonatorId",
                        column: x => x.DonatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00c8a77c-98d2-4864-88fd-b37f659e7133", 0, "afdad3c0-a01a-4298-a0ff-fb6fb657c5ee", "campaignCreator@mail.com", false, false, null, "CAMPAIGNCREATOR@MAIL.COM", "CAMPAIGNCREATOR@MAIL.COM", "AQAAAAEAACcQAAAAEELhvb1HtbCAVS/eN23STCIRNdYjhAAp0t24KEZnl453uzy8TydSe3mAWmMH/En3zA==", null, false, "70014bb8-9429-45d7-94ed-cedcf41d2331", false, "campaignCreator@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5d6ca01f-2556-4861-99f3-dbceb4e0bb99", 0, "38bb0c62-2842-443d-8894-31ca761d6252", "guest@mail.com", false, false, null, "GUEST@MAIL.COM", "GUEST@MAIL.COM", "AQAAAAEAACcQAAAAEO6CJH5oMF0iyx0rzsamr10K8BcfQwRO35qYxH/qK5MytZ/9eUUbVvi/B+MALS9pJQ==", null, false, "328a785e-e951-48a3-a7c1-358b0f0ba8cc", false, "guest@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "3844ff1c-ec37-4b72-8c5b-73a0542b85db", "campaignCreator2@mail.com", false, false, null, "CAMPAIGNCREATOR2@MAIL.COM", "CAMPAIGNCREATOR2@MAIL.COM", "AQAAAAEAACcQAAAAEDjEDZ5brEtOyuCxU2G/1VU4RH8KJKkGvVyHqeXN9hIRP5aFge8+MLIaR4nxpPm6Xw==", null, false, "baa312b9-b4eb-44c6-81f1-ea1c12c77c54", false, "campaignCreator2@mail.com" });

            migrationBuilder.InsertData(
                table: "CampaignCreators",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("3ed381b8-6973-4b97-94ab-988ab41458a9"), "00c8a77c-98d2-4864-88fd-b37f659e7133" });

            migrationBuilder.InsertData(
                table: "CampaignCreators",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("de2758f2-1c58-490c-8445-835b9dc348b0"), "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Id", "CampaignCreatorId", "Description", "GoalAmount", "Name" },
                values: new object[] { 1, new Guid("3ed381b8-6973-4b97-94ab-988ab41458a9"), "High school. Give us money because why not.", 69000m, "SoftUni Buditel" });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Id", "CampaignCreatorId", "Description", "GoalAmount", "Name" },
                values: new object[] { 2, new Guid("de2758f2-1c58-490c-8445-835b9dc348b0"), "Political party.", 100000m, "Gerb-SDS" });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "Amount", "CampaignId", "DonatorId" },
                values: new object[] { 1, 300m, 2, "5d6ca01f-2556-4861-99f3-dbceb4e0bb99" });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCreators_UserId",
                table: "CampaignCreators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CampaignCreatorId",
                table: "Campaigns",
                column: "CampaignCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CampaignId",
                table: "Donations",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonatorId",
                table: "Donations",
                column: "DonatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "CampaignCreators");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d6ca01f-2556-4861-99f3-dbceb4e0bb99");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00c8a77c-98d2-4864-88fd-b37f659e7133");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
