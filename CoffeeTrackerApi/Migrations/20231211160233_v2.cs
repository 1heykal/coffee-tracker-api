using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsumingnDate",
                table: "Records",
                newName: "ConsumingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsumingDate",
                table: "Records",
                newName: "ConsumingnDate");
        }
    }
}
