using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelMigrationCR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelTable_UserTable_NotesId",
                table: "LabelTable");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelTable_NotesTable_NotesId",
                table: "LabelTable",
                column: "NotesId",
                principalTable: "NotesTable",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelTable_NotesTable_NotesId",
                table: "LabelTable");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelTable_UserTable_NotesId",
                table: "LabelTable",
                column: "NotesId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
