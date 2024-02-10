using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class AddCrudsEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateRegionKml",
                schema: "General",
                table: "StateRegion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictKml",
                schema: "General",
                table: "District",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryKml",
                schema: "General",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClusterKml",
                schema: "General",
                table: "Cluster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityKml",
                schema: "General",
                table: "City",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Codec",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodecName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodecDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Codec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Codec_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataSource",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSourceDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_DataSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSource_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Domains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Domains_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExternalBaseStation",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalBaseStationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalBaseStationDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalBaseStationBTSType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalBaseStationRFSDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_ExternalBaseStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalBaseStation_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExternalOperator",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalOperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalOperatorDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ExternalOperator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalOperator_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KPICategory",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KPICategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPICategoryDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_KPICategory_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeasuringUnit",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringUnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasuringUnitDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MeasuringUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuringUnit_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperatorGroup",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorGroupDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_OperatorGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatorGroup_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Priority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priority_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Site",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Cluster_ClusterId",
                        column: x => x.ClusterId,
                        principalSchema: "General",
                        principalTable: "Cluster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Site_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubSystem",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubSystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubSystemDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SubSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSystem_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KPIFeedingLog",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KPIFeedingLogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIFeedingLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIFeedingLog_DataSource_DataSourceId",
                        column: x => x.DataSourceId,
                        principalSchema: "General",
                        principalTable: "DataSource",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPIFeedingLog_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Operator",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operator_OperatorGroup_OperatorGroupId",
                        column: x => x.OperatorGroupId,
                        principalSchema: "General",
                        principalTable: "OperatorGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operator_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cell",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessTechnologyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cell_AccessTechnology_AccessTechnologyId",
                        column: x => x.AccessTechnologyId,
                        principalSchema: "General",
                        principalTable: "AccessTechnology",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cell_Goal_GoalId",
                        column: x => x.GoalId,
                        principalSchema: "General",
                        principalTable: "Goal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cell_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "General",
                        principalTable: "Site",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cell_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cell_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "General",
                        principalTable: "Vendor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DieselGenerator",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DieselGeneratorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieselGeneratorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieselGeneratorCapacity = table.Column<double>(type: "float", nullable: false),
                    CapacityUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DieselGeneratorTankCapacity = table.Column<double>(type: "float", nullable: false),
                    TankCapacityUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_DieselGenerator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DieselGenerator_MeasuringUnit_CapacityUnitId",
                        column: x => x.CapacityUnitId,
                        principalSchema: "General",
                        principalTable: "MeasuringUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DieselGenerator_MeasuringUnit_TankCapacityUnitId",
                        column: x => x.TankCapacityUnitId,
                        principalSchema: "General",
                        principalTable: "MeasuringUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DieselGenerator_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "General",
                        principalTable: "Site",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DieselGenerator_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NPSKPIWeight",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NPSKPIWeightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPSKPIWeightDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "IX_Cell_AccessTechnologyId",
                schema: "General",
                table: "Cell",
                column: "AccessTechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_GoalId",
                schema: "General",
                table: "Cell",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_IsDeleted",
                schema: "General",
                table: "Cell",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_SiteId",
                schema: "General",
                table: "Cell",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_UserId",
                schema: "General",
                table: "Cell",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_VendorId",
                schema: "General",
                table: "Cell",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Codec_IsDeleted",
                schema: "General",
                table: "Codec",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Codec_UserId",
                schema: "General",
                table: "Codec",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_IsDeleted",
                schema: "General",
                table: "DataSource",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_UserId",
                schema: "General",
                table: "DataSource",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DieselGenerator_CapacityUnitId",
                schema: "General",
                table: "DieselGenerator",
                column: "CapacityUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DieselGenerator_IsDeleted",
                schema: "General",
                table: "DieselGenerator",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DieselGenerator_SiteId",
                schema: "General",
                table: "DieselGenerator",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_DieselGenerator_TankCapacityUnitId",
                schema: "General",
                table: "DieselGenerator",
                column: "TankCapacityUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DieselGenerator_UserId",
                schema: "General",
                table: "DieselGenerator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_IsDeleted",
                schema: "General",
                table: "Domains",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UserId",
                schema: "General",
                table: "Domains",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBaseStation_IsDeleted",
                schema: "General",
                table: "ExternalBaseStation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBaseStation_UserId",
                schema: "General",
                table: "ExternalBaseStation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalOperator_IsDeleted",
                schema: "General",
                table: "ExternalOperator",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalOperator_UserId",
                schema: "General",
                table: "ExternalOperator",
                column: "UserId");

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
                name: "IX_KPIFeedingLog_DataSourceId",
                schema: "General",
                table: "KPIFeedingLog",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIFeedingLog_IsDeleted",
                schema: "General",
                table: "KPIFeedingLog",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_KPIFeedingLog_UserId",
                schema: "General",
                table: "KPIFeedingLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnit_IsDeleted",
                schema: "General",
                table: "MeasuringUnit",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnit_UserId",
                schema: "General",
                table: "MeasuringUnit",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Operator_IsDeleted",
                schema: "General",
                table: "Operator",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Operator_OperatorGroupId",
                schema: "General",
                table: "Operator",
                column: "OperatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Operator_UserId",
                schema: "General",
                table: "Operator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorGroup_IsDeleted",
                schema: "General",
                table: "OperatorGroup",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorGroup_UserId",
                schema: "General",
                table: "OperatorGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Priority_IsDeleted",
                schema: "General",
                table: "Priority",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Priority_UserId",
                schema: "General",
                table: "Priority",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_IsDeleted",
                schema: "General",
                table: "Service",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Service_UserId",
                schema: "General",
                table: "Service",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_ClusterId",
                schema: "General",
                table: "Site",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_IsDeleted",
                schema: "General",
                table: "Site",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Site_UserId",
                schema: "General",
                table: "Site",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSystem_IsDeleted",
                schema: "General",
                table: "SubSystem",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SubSystem_UserId",
                schema: "General",
                table: "SubSystem",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cell",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Codec",
                schema: "General");

            migrationBuilder.DropTable(
                name: "DieselGenerator",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Domains",
                schema: "General");

            migrationBuilder.DropTable(
                name: "ExternalBaseStation",
                schema: "General");

            migrationBuilder.DropTable(
                name: "ExternalOperator",
                schema: "General");

            migrationBuilder.DropTable(
                name: "KPICategory",
                schema: "General");

            migrationBuilder.DropTable(
                name: "KPIFeedingLog",
                schema: "General");

            migrationBuilder.DropTable(
                name: "NPSKPIWeight",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Priority",
                schema: "General");

            migrationBuilder.DropTable(
                name: "SubSystem",
                schema: "General");

            migrationBuilder.DropTable(
                name: "MeasuringUnit",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Site",
                schema: "General");

            migrationBuilder.DropTable(
                name: "DataSource",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Operator",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "General");

            migrationBuilder.DropTable(
                name: "OperatorGroup",
                schema: "General");

            migrationBuilder.DropColumn(
                name: "StateRegionKml",
                schema: "General",
                table: "StateRegion");

            migrationBuilder.DropColumn(
                name: "DistrictKml",
                schema: "General",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CountryKml",
                schema: "General",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "ClusterKml",
                schema: "General",
                table: "Cluster");

            migrationBuilder.DropColumn(
                name: "CityKml",
                schema: "General",
                table: "City");
        }
    }
}
