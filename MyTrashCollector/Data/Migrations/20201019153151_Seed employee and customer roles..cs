using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrashCollector.Data.Migrations
{
    public partial class Seedemployeeandcustomerroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c276338-2427-4dc2-bf0a-4e7142d3e5d3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9c498008-a174-4cdb-9e92-f19743936d12", "ddba5e6c-f444-408f-9f9b-29a7c35d17bb", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1a186fe-ee22-4f2b-bf57-595c9bea75e2", "1faabc07-6b6b-4886-907e-1ecaa5185ba1", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c498008-a174-4cdb-9e92-f19743936d12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1a186fe-ee22-4f2b-bf57-595c9bea75e2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c276338-2427-4dc2-bf0a-4e7142d3e5d3", "935ea136-c7b9-4ea3-9fd3-715889c5dc47", "Admin", "ADMIN" });
        }
    }
}
