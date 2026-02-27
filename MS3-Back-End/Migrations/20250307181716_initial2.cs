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
            migrationBuilder.RenameColumn(
                name: "CteatedDate",
                table: "Students",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CteatedDate",
                table: "Admins",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Students",
                newName: "CteatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Admins",
                newName: "CteatedDate");
        }
    }
}
