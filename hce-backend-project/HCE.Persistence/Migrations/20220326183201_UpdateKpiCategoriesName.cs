using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class UpdateKpiCategoriesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPICategory_User_UserId",
                schema: "General",
                table: "KPICategory");

            migrationBuilder.DropForeignKey(
                name: "FK_KPICategory_KPI_KPIId",
                schema: "KPIs",
                table: "KPICategory");

            migrationBuilder.DropForeignKey(
                name: "FK_KPICategory_KPICategory_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KPICategory",
                schema: "KPIs",
                table: "KPICategory");

            migrationBuilder.RenameTable(
                name: "KPICategory",
                schema: "KPIs",
                newName: "KPICategories",
                newSchema: "KPIs");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_UserId1",
                schema: "General",
                table: "KPICategory",
                newName: "IX_KPICategory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_IsDeleted1",
                schema: "General",
                table: "KPICategory",
                newName: "IX_KPICategory_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_UserId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_KPIId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_KPIId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_KIKPICategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_IsDeleted",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KPICategories",
                schema: "KPIs",
                table: "KPICategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPICategories_KPI_KPIId",
                schema: "KPIs",
                table: "KPICategories",
                column: "KPIId",
                principalSchema: "KPIs",
                principalTable: "KPI",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPICategories_KPICategory_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategories",
                column: "KIKPICategoryId",
                principalSchema: "General",
                principalTable: "KPICategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPICategories_User_UserId",
                schema: "KPIs",
                table: "KPICategories",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPICategories_KPI_KPIId",
                schema: "KPIs",
                table: "KPICategories");

            migrationBuilder.DropForeignKey(
                name: "FK_KPICategories_KPICategory_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategories");

            migrationBuilder.DropForeignKey(
                name: "FK_KPICategories_User_UserId",
                schema: "KPIs",
                table: "KPICategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KPICategories",
                schema: "KPIs",
                table: "KPICategories");

            migrationBuilder.RenameTable(
                name: "KPICategories",
                schema: "KPIs",
                newName: "KPICategory",
                newSchema: "KPIs");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_UserId",
                schema: "General",
                table: "KPICategory",
                newName: "IX_KPICategory_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategory_IsDeleted",
                schema: "General",
                table: "KPICategory",
                newName: "IX_KPICategory_IsDeleted1");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_UserId",
                schema: "KPIs",
                table: "KPICategory",
                newName: "IX_KPICategory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_KPIId",
                schema: "KPIs",
                table: "KPICategory",
                newName: "IX_KPICategory_KPIId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategory",
                newName: "IX_KPICategory_KIKPICategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_IsDeleted",
                schema: "KPIs",
                table: "KPICategory",
                newName: "IX_KPICategory_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KPICategory",
                schema: "KPIs",
                table: "KPICategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPICategory_User_UserId",
                schema: "General",
                table: "KPICategory",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPICategory_KPI_KPIId",
                schema: "KPIs",
                table: "KPICategory",
                column: "KPIId",
                principalSchema: "KPIs",
                principalTable: "KPI",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPICategory_KPICategory_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategory",
                column: "KIKPICategoryId",
                principalSchema: "General",
                principalTable: "KPICategory",
                principalColumn: "Id");
        }
    }
}
