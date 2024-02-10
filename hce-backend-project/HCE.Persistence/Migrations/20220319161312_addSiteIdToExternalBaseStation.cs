using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class addSiteIdToExternalBaseStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SiteId",
                schema: "General",
                table: "ExternalBaseStation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBaseStation_SiteId",
                schema: "General",
                table: "ExternalBaseStation",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalBaseStation_Site_SiteId",
                schema: "General",
                table: "ExternalBaseStation",
                column: "SiteId",
                principalSchema: "General",
                principalTable: "Site",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalBaseStation_Site_SiteId",
                schema: "General",
                table: "ExternalBaseStation");

            migrationBuilder.DropIndex(
                name: "IX_ExternalBaseStation_SiteId",
                schema: "General",
                table: "ExternalBaseStation");

            migrationBuilder.DropColumn(
                name: "SiteId",
                schema: "General",
                table: "ExternalBaseStation");
        }
    }
}
