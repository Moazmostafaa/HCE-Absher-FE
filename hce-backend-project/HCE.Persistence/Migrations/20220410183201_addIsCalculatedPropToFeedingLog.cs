using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class addIsCalculatedPropToFeedingLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Cell_UserId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Customer_UserId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.AddColumn<bool>(
                name: "IsCalculated",
                schema: "KPIs",
                table: "KPIFeedingLog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_KPIFeedingLog_CellId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIFeedingLog_CustomerId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_Cell_CellId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "CellId",
                principalSchema: "General",
                principalTable: "Cell",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_Customer_CustomerId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "CustomerId",
                principalSchema: "General",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Cell_CellId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Customer_CustomerId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropIndex(
                name: "IX_KPIFeedingLog_CellId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropIndex(
                name: "IX_KPIFeedingLog_CustomerId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropColumn(
                name: "IsCalculated",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_Cell_UserId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Cell",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_Customer_UserId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
