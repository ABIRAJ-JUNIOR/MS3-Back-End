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
            migrationBuilder.AddColumn<bool>(
                name: "isReminder",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReminder",
                table: "Payments");
        }
    }
}
