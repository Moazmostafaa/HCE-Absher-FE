using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class removeCellIdFromKPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPI_Cell_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropColumn(
                name: "CellId",
                schema: "KPIs",
                table: "KPI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CellId",
                schema: "KPIs",
                table: "KPI",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_Cell_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Cell",
                principalColumn: "Id");
        }
    }
}
