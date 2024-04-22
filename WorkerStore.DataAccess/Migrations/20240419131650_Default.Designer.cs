using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DefaultDesigner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Divisions", columns: ["Id", "ParentID", "Name", "CreateDate", "Description"], values: [0, 0, "Default", DateTime.Now, "root division"]);
            migrationBuilder.InsertData(table: "Workers", columns: ["Id", "FirstName", "LastName", "MiddleName", "DateBithday", "Sex", "Post", "DriversLicense"], values: [0, "FirstName", "LastName", "MiddleName", DateTime.Today, "муж", "Post", false]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
