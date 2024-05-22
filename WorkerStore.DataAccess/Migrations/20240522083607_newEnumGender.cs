using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newEnumGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
