using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class npsResultCellRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NpsResult_CellId",
                table: "NpsResult",
                column: "CellId");

            migrationBuilder.AddForeignKey(
                name: "FK_NpsResult_Cell_CellId",
                table: "NpsResult",
                column: "CellId",
                principalSchema: "General",
                principalTable: "Cell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NpsResult_Cell_CellId",
                table: "NpsResult");

            migrationBuilder.DropIndex(
                name: "IX_NpsResult_CellId",
                table: "NpsResult");
        }
    }
}
