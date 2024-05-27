using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationTrackingSystem.Data.Migrations
{
    public partial class BugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCreators_AspNetUsers_UserId",
                table: "CampaignCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_CampaignCreators_CampaignCreatorId",
                table: "Campaigns");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00c8a77c-98d2-4864-88fd-b37f659e7133");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountDonated",
                table: "Campaigns",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "CampaignCreators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d6ca01f-2556-4861-99f3-dbceb4e0bb99",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2123e246-9b13-402f-800b-165e00ba4861", "AQAAAAEAACcQAAAAEE6RXNU93CoRk57Az+hi/jfqpA0lkG5ttsvPqyjZ4xP9jf520qoP9cS+HKxwzckHPA==", "1ab649e0-3cf3-4e51-9e99-08e87631781a" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3ed381b8-6973-4b97-94ab-988ab41458a9", 0, "df468763-785d-474f-a935-b62c7128f2b1", "campaignCreator@mail.com", false, false, null, "CAMPAIGNCREATOR@MAIL.COM", "CAMPAIGNCREATOR@MAIL.COM", "AQAAAAEAACcQAAAAEO/gFFRiBUmAXOBIlAGq800JYsYrp+qWZOkBw1wNC8AnW/oswWKj/+nrKdBLfB4BTg==", null, false, "3d24d8ed-1789-422e-a13a-a5eb9df37e58", false, "campaignCreator@mail.com" },
                    { "de2758f2-1c58-490c-8445-835b9dc348b0", 0, "0768b382-8687-4174-ba74-b363ed2d981b", "campaignCreator2@mail.com", false, false, null, "CAMPAIGNCREATOR2@MAIL.COM", "CAMPAIGNCREATOR2@MAIL.COM", "AQAAAAEAACcQAAAAEKZzU/5V146kV7oz9ZtNQ9s0BEoVPTIXDUovImBRkDqg4bkQMFCMxl6hicN4Ras9vQ==", null, false, "da970c83-d600-41f5-b511-9a19b1e20fcf", false, "campaignCreator2@mail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalAmountDonated",
                value: 300m);

            migrationBuilder.UpdateData(
                table: "CampaignCreators",
                keyColumn: "Id",
                keyValue: new Guid("3ed381b8-6973-4b97-94ab-988ab41458a9"),
                columns: new[] { "UserId", "Username" },
                values: new object[] { "3ed381b8-6973-4b97-94ab-988ab41458a9", "Chicho Krasi" });

            migrationBuilder.UpdateData(
                table: "CampaignCreators",
                keyColumn: "Id",
                keyValue: new Guid("de2758f2-1c58-490c-8445-835b9dc348b0"),
                columns: new[] { "UserId", "Username" },
                values: new object[] { "de2758f2-1c58-490c-8445-835b9dc348b0", "Boyko Borissov" });

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCreators_AspNetUsers_UserId",
                table: "CampaignCreators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_CampaignCreators_CampaignCreatorId",
                table: "Campaigns",
                column: "CampaignCreatorId",
                principalTable: "CampaignCreators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCreators_AspNetUsers_UserId",
                table: "CampaignCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_CampaignCreators_CampaignCreatorId",
                table: "Campaigns");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ed381b8-6973-4b97-94ab-988ab41458a9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de2758f2-1c58-490c-8445-835b9dc348b0");

            migrationBuilder.DropColumn(
                name: "TotalAmountDonated",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "CampaignCreators");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d6ca01f-2556-4861-99f3-dbceb4e0bb99",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38bb0c62-2842-443d-8894-31ca761d6252", "AQAAAAEAACcQAAAAEO6CJH5oMF0iyx0rzsamr10K8BcfQwRO35qYxH/qK5MytZ/9eUUbVvi/B+MALS9pJQ==", "328a785e-e951-48a3-a7c1-358b0f0ba8cc" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00c8a77c-98d2-4864-88fd-b37f659e7133", 0, "afdad3c0-a01a-4298-a0ff-fb6fb657c5ee", "campaignCreator@mail.com", false, false, null, "CAMPAIGNCREATOR@MAIL.COM", "CAMPAIGNCREATOR@MAIL.COM", "AQAAAAEAACcQAAAAEELhvb1HtbCAVS/eN23STCIRNdYjhAAp0t24KEZnl453uzy8TydSe3mAWmMH/En3zA==", null, false, "70014bb8-9429-45d7-94ed-cedcf41d2331", false, "campaignCreator@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "3844ff1c-ec37-4b72-8c5b-73a0542b85db", "campaignCreator2@mail.com", false, false, null, "CAMPAIGNCREATOR2@MAIL.COM", "CAMPAIGNCREATOR2@MAIL.COM", "AQAAAAEAACcQAAAAEDjEDZ5brEtOyuCxU2G/1VU4RH8KJKkGvVyHqeXN9hIRP5aFge8+MLIaR4nxpPm6Xw==", null, false, "baa312b9-b4eb-44c6-81f1-ea1c12c77c54", false, "campaignCreator2@mail.com" });

            migrationBuilder.UpdateData(
                table: "CampaignCreators",
                keyColumn: "Id",
                keyValue: new Guid("3ed381b8-6973-4b97-94ab-988ab41458a9"),
                column: "UserId",
                value: "00c8a77c-98d2-4864-88fd-b37f659e7133");

            migrationBuilder.UpdateData(
                table: "CampaignCreators",
                keyColumn: "Id",
                keyValue: new Guid("de2758f2-1c58-490c-8445-835b9dc348b0"),
                column: "UserId",
                value: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCreators_AspNetUsers_UserId",
                table: "CampaignCreators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_CampaignCreators_CampaignCreatorId",
                table: "Campaigns",
                column: "CampaignCreatorId",
                principalTable: "CampaignCreators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
