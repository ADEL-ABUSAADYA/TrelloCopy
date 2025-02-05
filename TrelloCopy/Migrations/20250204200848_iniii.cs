using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrelloCopy.Migrations
{
    /// <inheritdoc />
    public partial class iniii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKzYSHWib8JKYlQsSksieRbO49qYk+DGt+k7D7V7Mv69ObmD/Ffe7RNjJsHV35prlw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEG0fkd6C9a84CjLAHSZ5DjJq1J4AWb2i/uhT66whn83AObs5ndNaskU4b/y5YoqNzQ==");
        }
    }
}
