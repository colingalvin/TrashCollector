using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrashCollector.Data.Migrations
{
    public partial class AddIdentityUsertoEmployeeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d7e2d12-091c-48af-9831-0b827b983c9c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f55565c-ce85-4ca4-82af-7b6045206209");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Employees",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b6fa2a6-5783-41b5-9612-c4390bb3b1d5", "b6d5ee5b-7a62-4678-9f87-9cb198ca652c", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0dae36d3-5bf0-4496-b7af-1116293fcd94", "c009529f-9ae5-4c31-96e0-167f828d89ca", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdentityUserId",
                table: "Employees",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_IdentityUserId",
                table: "Employees",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_IdentityUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdentityUserId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dae36d3-5bf0-4496-b7af-1116293fcd94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b6fa2a6-5783-41b5-9612-c4390bb3b1d5");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d7e2d12-091c-48af-9831-0b827b983c9c", "a363f7f7-e3e3-4890-9ff2-2ef80d9441d2", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f55565c-ce85-4ca4-82af-7b6045206209", "aa603661-28b6-45f3-87dd-52760a824219", "Customer", "CUSTOMER" });
        }
    }
}
