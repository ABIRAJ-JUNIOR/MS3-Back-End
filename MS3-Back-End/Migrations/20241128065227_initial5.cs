using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS3_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssessmentLink",
                table: "Assessments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssessmentTitle",
                table: "Assessments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentLink",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "AssessmentTitle",
                table: "Assessments");
        }
    }
}
