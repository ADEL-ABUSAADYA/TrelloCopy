using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrelloCopy.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "UserFeatures",
                columns: new[] { "ID", "CreatedBy", "CreatedDate", "Deleted", "Feature", "UpdatedBy", "UpdatedDate", "UserID" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 0, null, 1 },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 0, null, 1 },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 0, null, 1 },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 0, null, 1 },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4, 0, null, 1 },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, 0, null, 1 },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6, 0, null, 1 },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 7, 0, null, 1 },
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 8, 0, null, 1 },
                    { 10, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 9, 0, null, 1 },
                    { 11, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 10, 0, null, 1 },
                    { 12, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 11, 0, null, 1 },
                    { 13, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12, 0, null, 1 },
                    { 14, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 13, 0, null, 1 },
                    { 15, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 14, 0, null, 1 },
                    { 16, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 15, 0, null, 1 },
                    { 17, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 16, 0, null, 1 },
                    { 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 101, 0, null, 1 },
                    { 19, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 102, 0, null, 1 },
                    { 20, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 103, 0, null, 1 },
                    { 21, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 104, 0, null, 1 },
                    { 22, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 105, 0, null, 1 },
                    { 23, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 106, 0, null, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "ResetPasswordToken" },
                values: new object[] { "AQAAAAIAAYagAAAAEDWv6fBsfngH/qPdsNs5dFqX+Wh3jHp1Qvff3050O9otRDLRBYKkITl0Q/Ztnd4MNQ==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "UserFeatures",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: null);
        }
    }
}
