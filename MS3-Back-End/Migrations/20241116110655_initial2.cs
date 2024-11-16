using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS3_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Assessments_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.DropColumn(
                name: "AssesmentId",
                table: "StudentAssessments");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssessmentId",
                table: "StudentAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Assessments_AssessmentId",
                table: "StudentAssessments",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Assessments_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssessmentId",
                table: "StudentAssessments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AssesmentId",
                table: "StudentAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Assessments_AssessmentId",
                table: "StudentAssessments",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id");
        }
    }
}
