using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class Kpi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KPIs");

            migrationBuilder.RenameTable(
                name: "KPIFeedingLog",
                schema: "General",
                newName: "KPIFeedingLog",
                newSchema: "KPIs");

            migrationBuilder.AddColumn<Guid>(
                name: "CellId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Value",
                schema: "KPIs",
                table: "KPIFeedingLog",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KPI",
                schema: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KPIName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIDefFormula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPITarget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIEntitity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalOptimization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIBadThreshold = table.Column<int>(type: "int", nullable: false),
                    KPIGoodThreshold = table.Column<int>(type: "int", nullable: false),
                    ISKPIExperienceCustomer = table.Column<bool>(type: "bit", nullable: false),
                    ISKPINPS = table.Column<bool>(type: "bit", nullable: false),
                    KPIGenericFormula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIVendorFormula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIDefaultWeight = table.Column<int>(type: "int", nullable: false),
                    KPICalculatedWeight = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KPICategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DomainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NPSKPIWeightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPI_Cell_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Cell",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_Codec_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Codec",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_Domains_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Domains",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_KPICategory_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "KPICategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_MeasuringUnit_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "MeasuringUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_NPSKPIWeight_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "NPSKPIWeight",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_Priority_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Priority",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_Service_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_SubSystem_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "SubSystem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPI_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CRMComplain",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MSISDN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketCreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolvedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPricePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRMComplain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CRMComplain_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "General",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMComplain_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KPIFeedingLog_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMComplain_CustomerId",
                schema: "General",
                table: "CRMComplain",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMComplain_IsDeleted",
                schema: "General",
                table: "CRMComplain",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CRMComplain_UserId",
                schema: "General",
                table: "CRMComplain",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IsDeleted",
                schema: "General",
                table: "Customer",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                schema: "General",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_IsDeleted",
                schema: "KPIs",
                table: "KPI",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_UserId",
                schema: "KPIs",
                table: "KPI",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_KPIFeedingLog_KPI_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog",
                column: "KPIId",
                principalSchema: "KPIs",
                principalTable: "KPI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Cell_UserId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_Customer_UserId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIFeedingLog_KPI_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropTable(
                name: "CRMComplain",
                schema: "General");

            migrationBuilder.DropTable(
                name: "KPI",
                schema: "KPIs");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "General");

            migrationBuilder.DropIndex(
                name: "IX_KPIFeedingLog_KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropColumn(
                name: "CellId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropColumn(
                name: "KPIId",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "KPIs",
                table: "KPIFeedingLog");

            migrationBuilder.RenameTable(
                name: "KPIFeedingLog",
                schema: "KPIs",
                newName: "KPIFeedingLog",
                newSchema: "General");
        }
    }
}
