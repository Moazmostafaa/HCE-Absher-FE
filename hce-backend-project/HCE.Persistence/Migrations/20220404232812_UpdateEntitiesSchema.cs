using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class UpdateEntitiesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Service",
                schema: "General",
                newName: "Service",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "Priority",
                schema: "General",
                newName: "Priority",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "OperatorGroup",
                schema: "General",
                newName: "OperatorGroup",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "Operator",
                schema: "General",
                newName: "Operator",
                newSchema: "KPIs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Service",
                schema: "KPIs",
                newName: "Service",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "Priority",
                schema: "KPIs",
                newName: "Priority",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "OperatorGroup",
                schema: "KPIs",
                newName: "OperatorGroup",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "Operator",
                schema: "KPIs",
                newName: "Operator",
                newSchema: "General");
        }
    }
}
