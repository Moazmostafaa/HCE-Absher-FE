using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class refactoringKpiAndServiceWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KpiWeight",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "ServiceWeights",
                schema: "KPIs");

            migrationBuilder.CreateTable(
                name: "Weight",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightValue = table.Column<double>(type: "float", nullable: false),
                    KpiId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Weight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weight_Kpi_KpiId",
                        column: x => x.KpiId,
                        principalSchema: "KPIs",
                        principalTable: "Kpi",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weight_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "KPIs",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weight_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weight_IsDeleted",
                schema: "KPIs",
                table: "Weight",
                column: "IsDeleted");

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

            migrationBuilder.CreateIndex(
                name: "IX_Weight_UserId",
                schema: "KPIs",
                table: "Weight",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weight",
                schema: "KPIs");

            migrationBuilder.CreateTable(
                name: "KpiWeight",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeightValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiWeight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiWeight_Kpi_KpiId",
                        column: x => x.KpiId,
                        principalSchema: "KPIs",
                        principalTable: "Kpi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KpiWeight_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "KPIs",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KpiWeight_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceWeights",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeightValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceWeights_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "KPIs",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceWeights_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KpiWeight_IsDeleted",
                schema: "KPIs",
                table: "KpiWeight",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_KpiWeight_KpiId",
                schema: "KPIs",
                table: "KpiWeight",
                column: "KpiId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiWeight_ServiceId",
                schema: "KPIs",
                table: "KpiWeight",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiWeight_UserId",
                schema: "KPIs",
                table: "KpiWeight",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_IsDeleted",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_ServiceId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_UserId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "UserId");
        }
    }
}
