using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class npsResultTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Weight_KpiId",
                schema: "KPIs",
                table: "Weight");

            migrationBuilder.DropIndex(
                name: "IX_Weight_ServiceId",
                schema: "KPIs",
                table: "Weight");

            migrationBuilder.CreateTable(
                name: "NpsResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NpsValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpsResult", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weight_KpiId",
                schema: "KPIs",
                table: "Weight",
                column: "KpiId",
                unique: true,
                filter: "[KpiId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_ServiceId",
                schema: "KPIs",
                table: "Weight",
                column: "ServiceId",
                unique: true,
                filter: "[ServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NpsResult_IsDeleted",
                table: "NpsResult",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NpsResult");

            migrationBuilder.DropIndex(
                name: "IX_Weight_KpiId",
                schema: "KPIs",
                table: "Weight");

            migrationBuilder.DropIndex(
                name: "IX_Weight_ServiceId",
                schema: "KPIs",
                table: "Weight");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_KpiId",
                schema: "KPIs",
                table: "Weight",
                column: "KpiId");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_ServiceId",
                schema: "KPIs",
                table: "Weight",
                column: "ServiceId");
        }
    }
}
