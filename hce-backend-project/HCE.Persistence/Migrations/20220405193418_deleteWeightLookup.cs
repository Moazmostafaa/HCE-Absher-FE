using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class deleteWeightLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KpiWeight_Weight_WeightId",
                schema: "KPIs",
                table: "KpiWeight");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceWeights_Weight_WeightId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropTable(
                name: "Weight",
                schema: "KPIs");

            migrationBuilder.DropIndex(
                name: "IX_ServiceWeights_WeightId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropIndex(
                name: "IX_KpiWeight_WeightId",
                schema: "KPIs",
                table: "KpiWeight");

            migrationBuilder.DropColumn(
                name: "WeightId",
                schema: "KPIs",
                table: "ServiceWeights");

            migrationBuilder.DropColumn(
                name: "WeightId",
                schema: "KPIs",
                table: "KpiWeight");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WeightId",
                schema: "KPIs",
                table: "ServiceWeights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WeightId",
                schema: "KPIs",
                table: "KpiWeight",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Weight",
                schema: "KPIs",
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
                        principalSchema: "KPIs",
                        principalTable: "Operator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weight_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWeights_WeightId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightId");

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
                name: "FK_KpiWeight_Weight_WeightId",
                schema: "KPIs",
                table: "KpiWeight",
                column: "WeightId",
                principalSchema: "KPIs",
                principalTable: "Weight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceWeights_Weight_WeightId",
                schema: "KPIs",
                table: "ServiceWeights",
                column: "WeightId",
                principalSchema: "KPIs",
                principalTable: "Weight",
                principalColumn: "Id");
        }
    }
}
