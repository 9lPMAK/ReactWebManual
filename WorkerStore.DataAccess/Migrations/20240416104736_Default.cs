using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerStore.DataAccess.Migrations;

/// <inheritdoc />
public partial class Default : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(table: "Divisions", columns: ["Id", "ParentID", "Name", "CreateDate", "Description"], values: [0, 0, "Default", DateTime.Now, "root division"]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}