using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrelloCopy.Migrations
{
    /// <inheritdoc />
    public partial class inint5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSprintItems");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "SprintItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEG0fkd6C9a84CjLAHSZ5DjJq1J4AWb2i/uhT66whn83AObs5ndNaskU4b/y5YoqNzQ==");

            migrationBuilder.CreateIndex(
                name: "IX_SprintItems_UserID",
                table: "SprintItems",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintItems_Users_UserID",
                table: "SprintItems",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintItems_Users_UserID",
                table: "SprintItems");

            migrationBuilder.DropIndex(
                name: "IX_SprintItems_UserID",
                table: "SprintItems");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "SprintItems");

            migrationBuilder.CreateTable(
                name: "UserSprintItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintItemID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSprintItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserSprintItems_SprintItems_SprintItemID",
                        column: x => x.SprintItemID,
                        principalTable: "SprintItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSprintItems_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAENXgEc6CqN1hU200W4tQeLEA/W54B3OimrXlQ4yQhShpeI/Tf/gF2V65zVqagolbIA==");

            migrationBuilder.CreateIndex(
                name: "IX_UserSprintItems_SprintItemID",
                table: "UserSprintItems",
                column: "SprintItemID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSprintItems_UserID",
                table: "UserSprintItems",
                column: "UserID");
        }
    }
}
