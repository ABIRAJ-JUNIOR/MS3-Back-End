using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS3_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class initial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollCount",
                table: "CourseSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollCount",
                table: "CourseSchedules");
        }
    }
}
