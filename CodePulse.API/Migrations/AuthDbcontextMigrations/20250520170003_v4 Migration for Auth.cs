using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDbcontextMigrations
{
    /// <inheritdoc />
    public partial class v4MigrationforAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEKPm/e4NqpDGXqY33Tuh3y1U7ikKPGJAdjld/eEnWFbQIT0caJBDX4icGeMjPK3x6A==", "5d937cd9-c4db-40c5-b354-cd05eab1c82b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEGnGhkDdE0rae0XmDCfSVu3WEGdDNaWVvWPpkL3uLlD2yAlvC+cEStQskGbWMnIFMw==", "12e7703e-65e2-4aac-be36-a2a595158862" });
        }
    }
}
