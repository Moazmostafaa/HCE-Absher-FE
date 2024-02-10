using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class refactorKpiForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Codec_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Domains_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_MeasuringUnit_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Priority_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Service_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_SubSystem_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_CodecId",
                schema: "KPIs",
                table: "Kpi",
                column: "CodecId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_DomainId",
                schema: "KPIs",
                table: "Kpi",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_MeasuringUnitId",
                schema: "KPIs",
                table: "Kpi",
                column: "MeasuringUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_PriorityId",
                schema: "KPIs",
                table: "Kpi",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_ServiceId",
                schema: "KPIs",
                table: "Kpi",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_SubSystemId",
                schema: "KPIs",
                table: "Kpi",
                column: "SubSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Codec_CodecId",
                schema: "KPIs",
                table: "Kpi",
                column: "CodecId",
                principalSchema: "KPIs",
                principalTable: "Codec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Domains_DomainId",
                schema: "KPIs",
                table: "Kpi",
                column: "DomainId",
                principalSchema: "KPIs",
                principalTable: "Domains",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_MeasuringUnit_MeasuringUnitId",
                schema: "KPIs",
                table: "Kpi",
                column: "MeasuringUnitId",
                principalSchema: "General",
                principalTable: "MeasuringUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Priority_PriorityId",
                schema: "KPIs",
                table: "Kpi",
                column: "PriorityId",
                principalSchema: "KPIs",
                principalTable: "Priority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Service_ServiceId",
                schema: "KPIs",
                table: "Kpi",
                column: "ServiceId",
                principalSchema: "KPIs",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_SubSystem_SubSystemId",
                schema: "KPIs",
                table: "Kpi",
                column: "SubSystemId",
                principalSchema: "KPIs",
                principalTable: "SubSystem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Codec_CodecId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Domains_DomainId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_MeasuringUnit_MeasuringUnitId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Priority_PriorityId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Service_ServiceId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_SubSystem_SubSystemId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropIndex(
                name: "IX_Kpi_CodecId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropIndex(
                name: "IX_Kpi_DomainId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropIndex(
                name: "IX_Kpi_MeasuringUnitId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropIndex(
                name: "IX_Kpi_PriorityId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropIndex(
                name: "IX_Kpi_ServiceId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropIndex(
                name: "IX_Kpi_SubSystemId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Codec_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "KPIs",
                principalTable: "Codec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Domains_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "KPIs",
                principalTable: "Domains",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_MeasuringUnit_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "General",
                principalTable: "MeasuringUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Priority_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "KPIs",
                principalTable: "Priority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Service_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "KPIs",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_SubSystem_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "KPIs",
                principalTable: "SubSystem",
                principalColumn: "Id");
        }
    }
}
