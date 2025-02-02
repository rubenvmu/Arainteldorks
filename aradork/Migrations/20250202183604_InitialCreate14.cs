using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aradork.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agenda",
                columns: table => new
                {
                    Firstname = table.Column<string>(type: "TEXT", unicode: false, maxLength: 364, nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", unicode: false, maxLength: 48, nullable: true),
                    URL = table.Column<string>(type: "TEXT", unicode: false, maxLength: 74, nullable: true),
                    Mail = table.Column<string>(type: "TEXT", unicode: false, maxLength: 39, nullable: true),
                    Company = table.Column<string>(type: "TEXT", unicode: false, maxLength: 68, nullable: true),
                    Postion = table.Column<string>(type: "TEXT", unicode: false, maxLength: 90, nullable: true),
                    LastConnection = table.Column<string>(type: "TEXT", unicode: false, maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__agenda__071F893BEFEC13FF", x => x.Firstname);
                });

            migrationBuilder.CreateTable(
                name: "AragonDorks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DorkValue = table.Column<string>(type: "TEXT", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AragonDorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Secrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secrets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agenda");

            migrationBuilder.DropTable(
                name: "AragonDorks");

            migrationBuilder.DropTable(
                name: "Secrets");
        }
    }
}
