using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class msOriginating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CRMComplain_Customer_CustomerId",
                schema: "General",
                table: "CRMComplain");

            migrationBuilder.DropForeignKey(
                name: "FK_CRMComplain_User_UserId",
                schema: "General",
                table: "CRMComplain");

            migrationBuilder.DropIndex(
                name: "IX_CRMComplain_CustomerId",
                schema: "General",
                table: "CRMComplain");

            migrationBuilder.DropIndex(
                name: "IX_CRMComplain_UserId",
                schema: "General",
                table: "CRMComplain");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "General",
                table: "CRMComplain");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "General",
                table: "CRMComplain");

            migrationBuilder.CreateTable(
                name: "MsOriginating",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateForStartOfCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeForStartOfCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeForStopOfCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstCallingLocationInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallingPartyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EosInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstAssignedSpeechCoderVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCauseAndLoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsOriginating", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MsOriginating_IsDeleted",
                schema: "General",
                table: "MsOriginating",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsOriginating",
                schema: "General");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "General",
                table: "CRMComplain",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "General",
                table: "CRMComplain",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CRMComplain_CustomerId",
                schema: "General",
                table: "CRMComplain",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMComplain_UserId",
                schema: "General",
                table: "CRMComplain",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CRMComplain_Customer_CustomerId",
                schema: "General",
                table: "CRMComplain",
                column: "CustomerId",
                principalSchema: "General",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CRMComplain_User_UserId",
                schema: "General",
                table: "CRMComplain",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
