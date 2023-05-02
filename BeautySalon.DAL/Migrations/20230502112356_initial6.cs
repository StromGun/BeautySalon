using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderServices");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "OrderServices",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "OrderServices",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderServices",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
