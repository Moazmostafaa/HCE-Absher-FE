using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class RefactorEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPI_Codec_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropForeignKey(
                name: "FK_KPI_Domains_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropForeignKey(
                name: "FK_KPI_MeasuringUnit_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropForeignKey(
                name: "FK_KPI_Priority_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropForeignKey(
                name: "FK_KPI_Service_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropForeignKey(
                name: "FK_KPI_SubSystem_UserId",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropForeignKey(
                name: "FK_KPI_User_UserId",
                schema: "KPIs",
                table: "KPI");

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

            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_KPI_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceWeights_Weights_WeightsId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropTable(
                name: "KPICategory",
                schema: "General");

            migrationBuilder.DropTable(
                name: "KPIWeights",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "Weights",
                schema: "General");

            migrationBuilder.DropIndex(
                name: "IX_ServiceWeights_WeightsId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KPICategories",
                schema: "KPIs",
                table: "KPICategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KPI",
                schema: "KPIs",
                table: "KPI");

            migrationBuilder.DropColumn(
                name: "WeightsId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.RenameTable(
                name: "KPICategories",
                schema: "KPIs",
                newName: "KpiCategories",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "KPI",
                schema: "KPIs",
                newName: "Kpi",
                newSchema: "KPIs");

            migrationBuilder.RenameColumn(
                name: "Weight",
                schema: "KPIs",
                table: "ServiceWeights",
                newName: "WeightValue");

            migrationBuilder.RenameColumn(
                name: "KPIId",
                schema: "KPIs",
                table: "KpiCategories",
                newName: "KpiId");

            migrationBuilder.RenameColumn(
                name: "KIKPICategoryId",
                schema: "KPIs",
                table: "KpiCategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_UserId",
                schema: "KPIs",
                table: "KpiCategories",
                newName: "IX_KpiCategories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_KPIId",
                schema: "KPIs",
                table: "KpiCategories",
                newName: "IX_KpiCategories_KpiId");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_IsDeleted",
                schema: "KPIs",
                table: "KpiCategories",
                newName: "IX_KpiCategories_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_KPICategories_KIKPICategoryId",
                schema: "KPIs",
                table: "KpiCategories",
                newName: "IX_KpiCategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "KPIVendorFormula",
                schema: "KPIs",
                table: "Kpi",
                newName: "VendorFormula");

            migrationBuilder.RenameColumn(
                name: "KPITarget",
                schema: "KPIs",
                table: "Kpi",
                newName: "Target");

            migrationBuilder.RenameColumn(
                name: "KPIName",
                schema: "KPIs",
                table: "Kpi",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "KPIGoodThreshold",
                schema: "KPIs",
                table: "Kpi",
                newName: "GoodThreshold");

            migrationBuilder.RenameColumn(
                name: "KPIGenericFormula",
                schema: "KPIs",
                table: "Kpi",
                newName: "GenericFormula");

            migrationBuilder.RenameColumn(
                name: "KPIEntitity",
                schema: "KPIs",
                table: "Kpi",
                newName: "Entity");

            migrationBuilder.RenameColumn(
                name: "KPIDesc",
                schema: "KPIs",
                table: "Kpi",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "KPIDefaultWeight",
                schema: "KPIs",
                table: "Kpi",
                newName: "DefaultWeight");

            migrationBuilder.RenameColumn(
                name: "KPIDefFormula",
                schema: "KPIs",
                table: "Kpi",
                newName: "DefinitionFormula");

            migrationBuilder.RenameColumn(
                name: "KPICalculatedWeight",
                schema: "KPIs",
                table: "Kpi",
                newName: "CalculatedWeight");

            migrationBuilder.RenameColumn(
                name: "KPIBadThreshold",
                schema: "KPIs",
                table: "Kpi",
                newName: "BadThreshold");

            migrationBuilder.RenameColumn(
                name: "KPIAbbreviation",
                schema: "KPIs",
                table: "Kpi",
                newName: "Abbreviation");

            migrationBuilder.RenameColumn(
                name: "ISKPINPS",
                schema: "KPIs",
                table: "Kpi",
                newName: "IsNps");

            migrationBuilder.RenameColumn(
                name: "ISKPIExperienceCustomer",
                schema: "KPIs",
                table: "Kpi",
                newName: "IsExperienceCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_KPI_UserId",
                schema: "KPIs",
                table: "Kpi",
                newName: "IX_Kpi_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KPI_IsDeleted",
                schema: "KPIs",
                table: "Kpi",
                newName: "IX_Kpi_IsDeleted");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentServiceId",
                schema: "General",
                table: "Service",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KpiCategories",
                schema: "KPIs",
                table: "KpiCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kpi",
                schema: "KPIs",
                table: "Kpi",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Weight",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_Weight_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weight_Operator_OperatorId",
                        column: x => x.OperatorId,
                        principalSchema: "General",
                        principalTable: "Operator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weight_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KpiWeight",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightValue = table.Column<double>(type: "float", nullable: false),
                    KpiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalSchema: "General",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KpiWeight_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KpiWeight_Weight_WeightId",
                        column: x => x.WeightId,
                        principalSchema: "KPIs",
                        principalTable: "Weight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_WeightId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ParentServiceId",
                schema: "General",
                table: "Service",
                column: "ParentServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsDeleted",
                schema: "KPIs",
                table: "Category",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                schema: "KPIs",
                table: "Category",
                column: "UserId");

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
                name: "IX_KpiWeight_WeightId",
                schema: "KPIs",
                table: "KpiWeight",
                column: "WeightId");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_CountryId",
                schema: "KPIs",
                table: "Weight",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_IsDeleted",
                schema: "KPIs",
                table: "Weight",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_OperatorId",
                schema: "KPIs",
                table: "Weight",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_UserId",
                schema: "KPIs",
                table: "Weight",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Codec_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Codec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Domains_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "General",
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
                principalSchema: "General",
                principalTable: "Priority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Service_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_SubSystem_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "General",
                principalTable: "SubSystem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_User_UserId",
                schema: "KPIs",
                table: "Kpi",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KpiCategories_Category_CategoryId",
                schema: "KPIs",
                table: "KpiCategories",
                column: "CategoryId",
                principalSchema: "KPIs",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KpiCategories_Kpi_KpiId",
                schema: "KPIs",
                table: "KpiCategories",
                column: "KpiId",
                principalSchema: "KPIs",
                principalTable: "Kpi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KpiCategories_User_UserId",
                schema: "KPIs",
                table: "KpiCategories",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_Kpi_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "KPIId",
                principalSchema: "KPIs",
                principalTable: "Kpi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Service_ParentServiceId",
                schema: "General",
                table: "Service",
                column: "ParentServiceId",
                principalSchema: "General",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceWeights_Weight_WeightId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightId",
                principalSchema: "KPIs",
                principalTable: "Weight",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_User_UserId",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_KpiCategories_Category_CategoryId",
                schema: "KPIs",
                table: "KpiCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_KpiCategories_Kpi_KpiId",
                schema: "KPIs",
                table: "KpiCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_KpiCategories_User_UserId",
                schema: "KPIs",
                table: "KpiCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Kpi_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Service_ParentServiceId",
                schema: "General",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceWeights_Weight_WeightId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "KpiWeight",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "Weight",
                schema: "KPIs");

            migrationBuilder.DropIndex(
                name: "IX_ServiceWeights_WeightId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropIndex(
                name: "IX_Service_ParentServiceId",
                schema: "General",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KpiCategories",
                schema: "KPIs",
                table: "KpiCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kpi",
                schema: "KPIs",
                table: "Kpi");

            migrationBuilder.DropColumn(
                name: "ParentServiceId",
                schema: "General",
                table: "Service");

            migrationBuilder.RenameTable(
                name: "KpiCategories",
                schema: "KPIs",
                newName: "KPICategories",
                newSchema: "KPIs");

            migrationBuilder.RenameTable(
                name: "Kpi",
                schema: "KPIs",
                newName: "KPI",
                newSchema: "KPIs");

            migrationBuilder.RenameColumn(
                name: "WeightValue",
                schema: "KPIs",
                table: "ServiceWeights",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "KpiId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "KPIId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "KIKPICategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_KpiCategories_UserId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KpiCategories_KpiId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_KPIId");

            migrationBuilder.RenameIndex(
                name: "IX_KpiCategories_IsDeleted",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_KpiCategories_CategoryId",
                schema: "KPIs",
                table: "KPICategories",
                newName: "IX_KPICategories_KIKPICategoryId");

            migrationBuilder.RenameColumn(
                name: "VendorFormula",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIVendorFormula");

            migrationBuilder.RenameColumn(
                name: "Target",
                schema: "KPIs",
                table: "KPI",
                newName: "KPITarget");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIName");

            migrationBuilder.RenameColumn(
                name: "IsNps",
                schema: "KPIs",
                table: "KPI",
                newName: "ISKPINPS");

            migrationBuilder.RenameColumn(
                name: "IsExperienceCustomer",
                schema: "KPIs",
                table: "KPI",
                newName: "ISKPIExperienceCustomer");

            migrationBuilder.RenameColumn(
                name: "GoodThreshold",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIGoodThreshold");

            migrationBuilder.RenameColumn(
                name: "GenericFormula",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIGenericFormula");

            migrationBuilder.RenameColumn(
                name: "Entity",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIEntitity");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIDesc");

            migrationBuilder.RenameColumn(
                name: "DefinitionFormula",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIDefFormula");

            migrationBuilder.RenameColumn(
                name: "DefaultWeight",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIDefaultWeight");

            migrationBuilder.RenameColumn(
                name: "CalculatedWeight",
                schema: "KPIs",
                table: "KPI",
                newName: "KPICalculatedWeight");

            migrationBuilder.RenameColumn(
                name: "BadThreshold",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIBadThreshold");

            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                schema: "KPIs",
                table: "KPI",
                newName: "KPIAbbreviation");

            migrationBuilder.RenameIndex(
                name: "IX_Kpi_UserId",
                schema: "KPIs",
                table: "KPI",
                newName: "IX_KPI_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Kpi_IsDeleted",
                schema: "KPIs",
                table: "KPI",
                newName: "IX_KPI_IsDeleted");

            migrationBuilder.AddColumn<Guid>(
                name: "WeightsId",
                schema: "KPIs",
                table: "ServiceWeights",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KPICategories",
                schema: "KPIs",
                table: "KPICategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KPI",
                schema: "KPIs",
                table: "KPI",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "KPICategory",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KPICategoryDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPICategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPICategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPICategory_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeightDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    KPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    WeightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_WeightsId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightsId");

            migrationBuilder.CreateIndex(
                name: "IX_KPICategory_IsDeleted",
                schema: "General",
                table: "KPICategory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_KPICategory_UserId",
                schema: "General",
                table: "KPICategory",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_Codec_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Codec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_Domains_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Domains",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_MeasuringUnit_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "MeasuringUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_Priority_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Priority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_Service_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_SubSystem_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "General",
                principalTable: "SubSystem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_User_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_KPI_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "KPIId",
                principalSchema: "KPIs",
                principalTable: "KPI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceWeights_Weights_WeightsId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightsId",
                principalSchema: "General",
                principalTable: "Weights",
                principalColumn: "Id");
        }
    }
}
