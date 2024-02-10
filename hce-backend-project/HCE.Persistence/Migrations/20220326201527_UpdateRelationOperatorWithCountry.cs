using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class UpdateRelationOperatorWithCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                schema: "General",
                table: "Weights",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "General",
                table: "Operator",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Operator_CountryId",
                schema: "General",
                table: "Operator",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operator_Country_CountryId",
                schema: "General",
                table: "Operator",
                column: "CountryId",
                principalSchema: "General",
                principalTable: "Country",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operator_Country_CountryId",
                schema: "General",
                table: "Operator");

            migrationBuilder.DropIndex(
                name: "IX_Operator_CountryId",
                schema: "General",
                table: "Operator");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "General",
                table: "Operator");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                schema: "General",
                table: "Weights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
