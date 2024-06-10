using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Araintelsoftware.Migrations
{
    /// <inheritdoc />
    public partial class RubenMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(

            name: "Birthdate",

            table: "AspNetUsers",

            nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
