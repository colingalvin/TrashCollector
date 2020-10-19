using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTrashCollector.Data.Migrations
{
    public partial class Combinestreetnumberandname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1049c99-837d-4545-bac3-80cec7a82b8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bded2811-b43a-4832-b896-0df7d452afe5");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Addresses",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d7e2d12-091c-48af-9831-0b827b983c9c", "a363f7f7-e3e3-4890-9ff2-2ef80d9441d2", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f55565c-ce85-4ca4-82af-7b6045206209", "aa603661-28b6-45f3-87dd-52760a824219", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d7e2d12-091c-48af-9831-0b827b983c9c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f55565c-ce85-4ca4-82af-7b6045206209");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bded2811-b43a-4832-b896-0df7d452afe5", "1d3d6e3e-c085-43b1-9af4-6ceb305b3b86", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1049c99-837d-4545-bac3-80cec7a82b8e", "f188a40f-c139-410c-b82f-cf63e12b601b", "Customer", "CUSTOMER" });
        }
    }
}
