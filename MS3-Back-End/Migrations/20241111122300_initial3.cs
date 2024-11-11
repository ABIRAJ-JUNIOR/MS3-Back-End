using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS3_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_UserRoles_UserRoleId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_UserRoles_UserRoleId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserRoleId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserRoleId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Admins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserRoleId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserRoleId",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserRoleId",
                table: "Students",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserRoleId",
                table: "Admins",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_UserRoles_UserRoleId",
                table: "Admins",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_UserRoles_UserRoleId",
                table: "Students",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
