using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProje.Data.Migrations
{
    public partial class blogforeignkeyeklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Blog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_ApplicationUserId",
                table: "Blog",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_ApplicationUserId",
                table: "Blog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_ApplicationUserId",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_ApplicationUserId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Blog");
        }
    }
}
