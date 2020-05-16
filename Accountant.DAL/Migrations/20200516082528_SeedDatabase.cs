using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountant.DAL.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "ShoppingLists",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "testDescription", "testCategory" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "testGroup" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "test@email.com", "testUser", new byte[] {  }, new byte[] {  } });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "EndDate", "EvaluationDate", "GroupId", "StartDate" },
                values: new object[] { 1, new DateTime(1997, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, new DateTime(1996, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ShoppingLists",
                columns: new[] { "Id", "GroupId", "Name" },
                values: new object[] { 1, 1, "testList" });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "UserId", "GroupId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "CategoryId", "PurchaseDate", "ReportId", "UserId" },
                values: new object[] { 1, 1545, 1, new DateTime(1996, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "ShoppingListItems",
                columns: new[] { "Id", "IsTicked", "Name", "ShoppingListId" },
                values: new object[] { 1, false, "testItem1", 1 });

            migrationBuilder.InsertData(
                table: "ShoppingListItems",
                columns: new[] { "Id", "IsTicked", "Name", "ShoppingListId" },
                values: new object[] { 2, true, "testItem2", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingListItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingListItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumns: new[] { "UserId", "GroupId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "ShoppingLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
