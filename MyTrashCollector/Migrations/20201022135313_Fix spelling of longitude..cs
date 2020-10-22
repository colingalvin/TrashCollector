using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrashCollector.Migrations
{
    public partial class Fixspellingoflongitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Longitute",
                table: "Addresses");

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Addresses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51c4f836-fe79-42b4-8d36-3d98aef18c36", "1b7467ed-0770-4e0a-b3f9-5b77ab080de3", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b30268a-7b28-41da-a8aa-500d5ef284c9", "2f437af8-345d-44fc-9b63-1660631a7f60", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b30268a-7b28-41da-a8aa-500d5ef284c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51c4f836-fe79-42b4-8d36-3d98aef18c36");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Addresses");

            migrationBuilder.AddColumn<double>(
                name: "Longitute",
                table: "Addresses",
                type: "float",
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
    }
}
