using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace task_management_system.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e995991-e5ce-4b73-9001-a49c3f4fc36d", "a71ab93a-6dfc-45ec-9066-125fb51de007", "Admin", "ADMIN" },
                    { "eff47076-d0eb-4922-80a8-6db7ff821954", "14s3eec47-c1c1-4729-af69-23656139ffdd", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e995991-e5ce-4b73-9001-a49c3f4fc36d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eff47076-d0eb-4922-80a8-6db7ff821954");
        }
    }
}
