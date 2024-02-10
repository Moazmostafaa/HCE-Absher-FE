using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class UpdateTheRelationOfWeightWithServiceAndKPIs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPI_NPSKPIWeight_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropTable(
                name: "NPSKPIWeight",
                schema: "General");

            migrationBuilder.DropColumn(
                name: "NPSKPIWeightId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.CreateTable(
                name: "Weights",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weights_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weights_Operator_OperatorId",
                        column: x => x.OperatorId,
                        principalSchema: "General",
                        principalTable: "Operator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weights_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KPIWeights",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    KPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_KPIWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIWeights_KPI_KPIId",
                        column: x => x.KPIId,
                        principalSchema: "KPIs",
                        principalTable: "KPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPIWeights_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "General",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPIWeights_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPIWeights_Weights_WeightsId",
                        column: x => x.WeightsId,
                        principalSchema: "General",
                        principalTable: "Weights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceWeights",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ServiceWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceWeights_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "General",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceWeights_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceWeights_Weights_WeightsId",
                        column: x => x.WeightsId,
                        principalSchema: "General",
                        principalTable: "Weights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KPIWeights_IsDeleted",
                schema: "KPIs",
                table: "KPIWeights",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_KPIWeights_KPIId",
                schema: "KPIs",
                table: "KPIWeights",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIWeights_ServiceId",
                schema: "KPIs",
                table: "KPIWeights",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIWeights_UserId",
                schema: "KPIs",
                table: "KPIWeights",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIWeights_WeightsId",
                schema: "KPIs",
                table: "KPIWeights",
                column: "WeightsId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_WeightsId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightsId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_CountryId",
                schema: "General",
                table: "Weights",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_IsDeleted",
                schema: "General",
                table: "Weights",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_OperatorId",
                schema: "General",
                table: "Weights",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_UserId",
                schema: "General",
                table: "Weights",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KPIWeights",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "ServiceWeights",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "Weights",
                schema: "General");

            migrationBuilder.AddColumn<Guid>(
                name: "NPSKPIWeightId",
                schema: "KPIs",
                table: "KPI",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NPSKPIWeight",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NPSKPIWeightDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPSKPIWeightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPSKPIWeight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPSKPIWeight_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPSKPIWeight_Operator_OperatorId",
                        column: x => x.OperatorId,
                        principalSchema: "General",
                        principalTable: "Operator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPSKPIWeight_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "General",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPSKPIWeight_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NPSKPIWeight_CountryId",
                schema: "General",
                table: "NPSKPIWeight",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_NPSKPIWeight_IsDeleted",
                schema: "General",
                table: "NPSKPIWeight",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_NPSKPIWeight_OperatorId",
                schema: "General",
                table: "NPSKPIWeight",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_NPSKPIWeight_ServiceId",
                schema: "General",
                table: "NPSKPIWeight",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_NPSKPIWeight_UserId",
                schema: "General",
                table: "NPSKPIWeight",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_NPSKPIWeight_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "NPSKPIWeight",
                principalColumn: "Id");
        }
    }
}
