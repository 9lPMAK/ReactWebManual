using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeIsDriversLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriversLicense",
                table: "Workers",
                newName: "IsDriversLicense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDriversLicense",
                table: "Workers",
                newName: "DriversLicense");
        }
    }
}
