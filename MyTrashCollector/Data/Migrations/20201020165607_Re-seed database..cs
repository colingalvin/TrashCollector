using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrashCollector.Data.Migrations
{
    public partial class Reseeddatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dae36d3-5bf0-4496-b7af-1116293fcd94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b6fa2a6-5783-41b5-9612-c4390bb3b1d5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8fdeeca-97f3-4fec-a3d3-5c724bd6105d", "e7309b2f-a9a2-42af-b0f2-ec63f0ed33f4", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c9597778-672a-4c21-a0c0-fba5fc764a23", "9b8316fe-2198-4b48-b2ca-888891fde7ee", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9597778-672a-4c21-a0c0-fba5fc764a23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8fdeeca-97f3-4fec-a3d3-5c724bd6105d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b6fa2a6-5783-41b5-9612-c4390bb3b1d5", "b6d5ee5b-7a62-4678-9f87-9cb198ca652c", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0dae36d3-5bf0-4496-b7af-1116293fcd94", "c009529f-9ae5-4c31-96e0-167f828d89ca", "Customer", "CUSTOMER" });
        }
    }
}
