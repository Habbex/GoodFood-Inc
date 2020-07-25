using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class UserLoginDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserLogins_UserLoginId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserLoginId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserLoginId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserLoginForeignKey",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLoginForeignKey",
                table: "Users",
                column: "UserLoginForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserLogins_UserLoginForeignKey",
                table: "Users",
                column: "UserLoginForeignKey",
                principalTable: "UserLogins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserLogins_UserLoginForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserLoginForeignKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserLoginForeignKey",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserLoginId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLoginId",
                table: "Users",
                column: "UserLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserLogins_UserLoginId",
                table: "Users",
                column: "UserLoginId",
                principalTable: "UserLogins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
