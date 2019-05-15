using Microsoft.EntityFrameworkCore.Migrations;

namespace milkrate.Migrations
{
    public partial class ConditionWork2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "UserPiece",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "UserPiece",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
