using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrashCollector.Migrations
{
    public partial class Addaddresslatlng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "565cda75-6250-4ddf-8f3b-36ed6dfd5df5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ee8cd79-2785-4803-877a-a1e902e215fd");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Addresses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitute",
                table: "Addresses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84842aa0-3c8b-4319-bd05-9b1f59a438fb", "42befdcf-0652-45c6-9649-998bf7bd31d5", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e1ff2f4-8ed9-4d8b-82af-5b0c67757ae3", "637340e2-12d4-40d6-84d8-9b57d2bc9612", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e1ff2f4-8ed9-4d8b-82af-5b0c67757ae3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84842aa0-3c8b-4319-bd05-9b1f59a438fb");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitute",
                table: "Addresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ee8cd79-2785-4803-877a-a1e902e215fd", "e7f35962-5b5c-448d-bfad-0e2b788b6277", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "565cda75-6250-4ddf-8f3b-36ed6dfd5df5", "65710eaf-b247-41e8-b2e3-4d76e71040fe", "Customer", "CUSTOMER" });
        }
    }
}
