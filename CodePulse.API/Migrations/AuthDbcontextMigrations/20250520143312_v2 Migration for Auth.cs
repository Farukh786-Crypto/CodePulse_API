using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDbcontextMigrations
{
    /// <inheritdoc />
    public partial class v2MigrationforAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A1A1A1A1-B2B2-C3C3-D4D4-E5E5E5E5E5E5",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Reader", "READER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B1B1B1B1-C2C2-D3D3-E4E4-F5F5F5F5F5F5",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Writer", "WRITER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEJeB345zwVlxxaMsvmNaP4GpDXzg12K/ZqbCxR50GIVGWeuUEz3dzntcZDc8cgZsAw==", "37d57abb-9db4-47d4-b088-dc846e67c7f8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A1A1A1A1-B2B2-C3C3-D4D4-E5E5E5E5E5E5",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "A1A1A1A1-B2B2-C3C3-D4D4-E5E5E5E5E5E5", "A1A1A1A1-B2B2-C3C3-D4D4-E5E5E5E5E5E5" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B1B1B1B1-C2C2-D3D3-E4E4-F5F5F5F5F5F5",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "B1B1B1B1-C2C2-D3D3-E4E4-F5F5F5F5F5F5", "B1B1B1B1-C2C2-D3D3-E4E4-F5F5F5F5F5F5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C1C1C1C1-D2D2-E3E3-F4F4-A5A5A5A5A5A5",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEPsQMeuMxaqHjoFVOlNQAZjdH2C7qr3yjE/SoYxgHgNiVHA+aWLsK1wQ8cXMG9sQ1g==", "3adcf13e-afce-41e5-a530-dfcb28330929" });
        }
    }
}
