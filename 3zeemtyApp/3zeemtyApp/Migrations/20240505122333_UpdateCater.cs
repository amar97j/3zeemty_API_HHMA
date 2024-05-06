using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3zeemtyApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "Cateres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Cateres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cateres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Item",
                value: "FOOD");
        }
    }
}
