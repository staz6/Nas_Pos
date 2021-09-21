using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ledgerChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Ledgers",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<double>(
                name: "AmountPaid",
                table: "Ledgers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AmountRemaining",
                table: "Ledgers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "AmountRemaining",
                table: "Ledgers");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Ledgers",
                newName: "Amount");
        }
    }
}
