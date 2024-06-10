using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Araintelsoftware.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBirthdateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
