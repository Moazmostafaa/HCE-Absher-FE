using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class updateKpiEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SubSystem",
                schema: "General",
                newName: "SubSystem",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "Domains",
                schema: "General",
                newName: "Domains",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "Codec",
                schema: "General",
                newName: "Codec",
                newSchema: "KPIs");

            migrationBuilder.AlterColumn<double>(
                name: "GoodThreshold",
                schema: "KPIs",
                table: "Kpi",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "DefaultWeight",
                schema: "KPIs",
                table: "Kpi",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "CalculatedWeight",
                schema: "KPIs",
                table: "Kpi",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "BadThreshold",
                schema: "KPIs",
                table: "Kpi",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SubSystem",
                schema: "KPIs",
                newName: "SubSystem",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "Domains",
                schema: "KPIs",
                newName: "Domains",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "Codec",
                schema: "KPIs",
                newName: "Codec",
                newSchema: "General");

            migrationBuilder.AlterColumn<int>(
                name: "GoodThreshold",
                schema: "KPIs",
                table: "Kpi",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultWeight",
                schema: "KPIs",
                table: "Kpi",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "CalculatedWeight",
                schema: "KPIs",
                table: "Kpi",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "BadThreshold",
                schema: "KPIs",
                table: "Kpi",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
