using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProje.Data.Migrations
{
    public partial class AddBlog1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Homeland_StudentId",
                table: "Blog");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Student_StudentId",
                table: "Blog",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Student_StudentId",
                table: "Blog");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Homeland_StudentId",
                table: "Blog",
                column: "StudentId",
                principalTable: "Homeland",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
