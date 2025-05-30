using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDbcontextMigrations
{
    /// <inheritdoc />
    public partial class v3MigrationforAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEGnGhkDdE0rae0XmDCfSVu3WEGdDNaWVvWPpkL3uLlD2yAlvC+cEStQskGbWMnIFMw==", "12e7703e-65e2-4aac-be36-a2a595158862" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEJeB345zwVlxxaMsvmNaP4GpDXzg12K/ZqbCxR50GIVGWeuUEz3dzntcZDc8cgZsAw==", "37d57abb-9db4-47d4-b088-dc846e67c7f8" });
        }
    }
}
