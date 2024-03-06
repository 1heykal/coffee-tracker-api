using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class addedIdenttity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiresAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VerificationToken",
                table: "AspNetUsers",
                newName: "Gender");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_ApplicationUserId",
                table: "Records",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_ApplicationUserId",
                table: "Records",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_ApplicationUserId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ApplicationUserId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "VerificationToken");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiresAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
