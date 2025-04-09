using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class update_whole_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId1",
                table: "CarImages");

            migrationBuilder.DropForeignKey(
                name: "FK_WashOrders_Cars_CarId",
                table: "WashOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WashOrders_Users_UserId",
                table: "WashOrders");

            migrationBuilder.DropIndex(
                name: "IX_CarImages_CarId1",
                table: "CarImages");

            migrationBuilder.DropColumn(
                name: "CarId1",
                table: "CarImages");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "Notifications",
                newName: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "WashOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WashOrders_WasherId",
                table: "WashOrders",
                column: "WasherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WashOrders_Cars_CarId",
                table: "WashOrders",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WashOrders_Users_UserId",
                table: "WashOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WashOrders_Users_WasherId",
                table: "WashOrders",
                column: "WasherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages");

            migrationBuilder.DropForeignKey(
                name: "FK_WashOrders_Cars_CarId",
                table: "WashOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WashOrders_Users_UserId",
                table: "WashOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WashOrders_Users_WasherId",
                table: "WashOrders");

            migrationBuilder.DropIndex(
                name: "IX_WashOrders_WasherId",
                table: "WashOrders");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Notifications",
                newName: "message");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "WashOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "User");

            migrationBuilder.AddColumn<int>(
                name: "CarId1",
                table: "CarImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_CarId1",
                table: "CarImages",
                column: "CarId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId1",
                table: "CarImages",
                column: "CarId1",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WashOrders_Cars_CarId",
                table: "WashOrders",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WashOrders_Users_UserId",
                table: "WashOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
