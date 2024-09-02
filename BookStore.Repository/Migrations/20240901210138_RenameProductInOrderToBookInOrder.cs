using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductInOrderToBookInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_OwnerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInOrder_Books_BookId",
                table: "ProductInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInOrder_Order_OrderId",
                table: "ProductInOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInOrder",
                table: "ProductInOrder");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "ProductInOrder",
                newName: "BookInOrder");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInOrder_OrderId",
                table: "BookInOrder",
                newName: "IX_BookInOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInOrder_BookId",
                table: "BookInOrder",
                newName: "IX_BookInOrder_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookInOrder",
                table: "BookInOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInOrder_Books_BookId",
                table: "BookInOrder",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInOrder_Order_OrderId",
                table: "BookInOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_OwnerId",
                table: "Order",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInOrder_Books_BookId",
                table: "BookInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInOrder_Order_OrderId",
                table: "BookInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_OwnerId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookInOrder",
                table: "BookInOrder");

            migrationBuilder.RenameTable(
                name: "BookInOrder",
                newName: "ProductInOrder");

            migrationBuilder.RenameIndex(
                name: "IX_BookInOrder_OrderId",
                table: "ProductInOrder",
                newName: "IX_ProductInOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_BookInOrder_BookId",
                table: "ProductInOrder",
                newName: "IX_ProductInOrder_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInOrder",
                table: "ProductInOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_OwnerId",
                table: "Order",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInOrder_Books_BookId",
                table: "ProductInOrder",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInOrder_Order_OrderId",
                table: "ProductInOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
