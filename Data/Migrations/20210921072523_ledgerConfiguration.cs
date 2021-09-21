using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ledgerConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Ledgers_LedgerId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Ledgers_LedgerId",
                table: "Transactions",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Ledgers_LedgerId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Ledgers_LedgerId",
                table: "Transactions",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
