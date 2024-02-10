using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class UpdateRegionModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Region",
                schema: "General");

            migrationBuilder.CreateTable(
                name: "WorldRegion",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorldRegionNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorldRegionNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorldRegionNameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorldRegionDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_WorldRegion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorldRegion_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryNameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordRegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_WorldRegion_WordRegionId",
                        column: x => x.WordRegionId,
                        principalSchema: "General",
                        principalTable: "WorldRegion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StateRegion",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateRegionNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateRegionNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateRegionNameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateRegionDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_StateRegion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateRegion_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StateRegion_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityNameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTop = table.Column<bool>(type: "bit", nullable: false),
                    StateRegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_StateRegion_StateRegionId",
                        column: x => x.StateRegionId,
                        principalSchema: "General",
                        principalTable: "StateRegion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_City_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "District",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictNameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "General",
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_District_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cluster",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClusterNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClusterNameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClusterDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Cluster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cluster_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "General",
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cluster_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_IsDeleted",
                schema: "General",
                table: "City",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateRegionId",
                schema: "General",
                table: "City",
                column: "StateRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_City_UserId",
                schema: "General",
                table: "City",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cluster_DistrictId",
                schema: "General",
                table: "Cluster",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Cluster_IsDeleted",
                schema: "General",
                table: "Cluster",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Cluster_UserId",
                schema: "General",
                table: "Cluster",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_IsDeleted",
                schema: "General",
                table: "Country",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UserId",
                schema: "General",
                table: "Country",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_WordRegionId",
                schema: "General",
                table: "Country",
                column: "WordRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CityId",
                schema: "General",
                table: "District",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_District_IsDeleted",
                schema: "General",
                table: "District",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_District_UserId",
                schema: "General",
                table: "District",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StateRegion_CountryId",
                schema: "General",
                table: "StateRegion",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StateRegion_IsDeleted",
                schema: "General",
                table: "StateRegion",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StateRegion_UserId",
                schema: "General",
                table: "StateRegion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorldRegion_IsDeleted",
                schema: "General",
                table: "WorldRegion",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WorldRegion_UserId",
                schema: "General",
                table: "WorldRegion",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cluster",
                schema: "General");

            migrationBuilder.DropTable(
                name: "District",
                schema: "General");

            migrationBuilder.DropTable(
                name: "City",
                schema: "General");

            migrationBuilder.DropTable(
                name: "StateRegion",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "General");

            migrationBuilder.DropTable(
                name: "WorldRegion",
                schema: "General");

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsTop = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Region_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "General",
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Region_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Region_IsDeleted",
                schema: "General",
                table: "Region",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Region_ParentId",
                schema: "General",
                table: "Region",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_UserId",
                schema: "General",
                table: "Region",
                column: "UserId");
        }
    }
}
