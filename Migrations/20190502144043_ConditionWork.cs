using Microsoft.EntityFrameworkCore.Migrations;

namespace milkrate.Migrations
{
    public partial class ConditionWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPiece_ConditionId",
                table: "UserPiece",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPiece_Condition_ConditionId",
                table: "UserPiece",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPiece_Condition_ConditionId",
                table: "UserPiece");

            migrationBuilder.DropIndex(
                name: "IX_UserPiece_ConditionId",
                table: "UserPiece");
        }
    }
}
