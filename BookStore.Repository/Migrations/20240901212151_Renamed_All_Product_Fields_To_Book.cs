using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_All_Product_Fields_To_Book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInOrder_Books_BookId",
                table: "BookInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInShoppingCarts_Books_BookId",
                table: "BookInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BookInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BookInOrder");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "BookInShoppingCarts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "BookInOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInOrder_Books_BookId",
                table: "BookInOrder",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInShoppingCarts_Books_BookId",
                table: "BookInShoppingCarts",
                column: "BookId",
                principalTable: "Books",
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
                name: "FK_BookInShoppingCarts_Books_BookId",
                table: "BookInShoppingCarts");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "BookInShoppingCarts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "BookInShoppingCarts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "BookInOrder",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "BookInOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_BookInOrder_Books_BookId",
                table: "BookInOrder",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInShoppingCarts_Books_BookId",
                table: "BookInShoppingCarts",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
