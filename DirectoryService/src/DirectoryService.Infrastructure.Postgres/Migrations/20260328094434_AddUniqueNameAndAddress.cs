using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueNameAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ux_locations_address",
                table: "locations",
                columns: new[] { "country", "city", "street", "house_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_locations_name",
                table: "locations",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_locations_address",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ux_locations_name",
                table: "locations");
        }
    }
}
