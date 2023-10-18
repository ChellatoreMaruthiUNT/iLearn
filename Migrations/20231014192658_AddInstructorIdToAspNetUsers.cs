using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iLearn.Migrations
{
    public partial class AddInstructorIdToAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "AspNetUsers");
        }
    }
}
