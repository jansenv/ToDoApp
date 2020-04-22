using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ToDoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Not Complete" });

            migrationBuilder.InsertData(
                table: "ToDoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "ToDoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Complete" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
