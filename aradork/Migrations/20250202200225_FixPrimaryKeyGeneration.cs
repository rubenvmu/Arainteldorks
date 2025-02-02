using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aradork.Migrations
{
    /// <inheritdoc />
    public partial class FixPrimaryKeyGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__agenda__071F893BEFEC13FF",
                table: "agenda");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AragonDorks",
                type: "TEXT",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DorkValue",
                table: "AragonDorks",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "AragonDorks",
                type: "TEXT",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agenda_Firstname",
                table: "agenda",
                column: "Firstname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Agenda_Firstname",
                table: "agenda");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AragonDorks",
                type: "TEXT",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DorkValue",
                table: "AragonDorks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "AragonDorks",
                type: "TEXT",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK__agenda__071F893BEFEC13FF",
                table: "agenda",
                column: "Firstname");
        }
    }
}
