using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class AddKPICategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPI_KPICategory_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropColumn(
                name: "KPICategoryId",
                schema: "KPIs",
                table: "KPI");

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

            migrationBuilder.CreateTable(
                name: "KPICategory",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KIKPICategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPICategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPICategory_KPI_KPIId",
                        column: x => x.KPIId,
                        principalSchema: "KPIs",
                        principalTable: "KPI",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPICategory_KPICategory_KIKPICategoryId",
                        column: x => x.KIKPICategoryId,
                        principalSchema: "General",
                        principalTable: "KPICategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPICategory_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KPICategory_IsDeleted",
                schema: "KPIs",
                table: "KPICategory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_KPICategory_KIKPICategoryId",
                schema: "KPIs",
                table: "KPICategory",
                column: "KIKPICategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KPICategory_KPIId",
                schema: "KPIs",
                table: "KPICategory",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_KPICategory_UserId",
                schema: "KPIs",
                table: "KPICategory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPICategory_User_UserId",
                schema: "General",
                table: "KPICategory");

            migrationBuilder.DropTable(
                name: "KPICategory",
                schema: "KPIs");

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

            migrationBuilder.AddColumn<Guid>(
                name: "KPICategoryId",
                schema: "KPIs",
                table: "KPI",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_KPICategory_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "KPICategory",
                principalColumn: "Id");
        }
    }
}
