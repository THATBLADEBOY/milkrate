using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace milkrate.Migrations
{
    public partial class trackedvalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackedValue",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(nullable: false),
                    TrackedDate = table.Column<DateTime>(nullable: false),
                    PieceId = table.Column<int>(nullable: false),
                    UserPieceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackedValue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrackedValue_UserPiece_UserPieceId",
                        column: x => x.UserPieceId,
                        principalTable: "UserPiece",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackedValue_UserPieceId",
                table: "TrackedValue",
                column: "UserPieceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackedValue");
        }
    }
}
