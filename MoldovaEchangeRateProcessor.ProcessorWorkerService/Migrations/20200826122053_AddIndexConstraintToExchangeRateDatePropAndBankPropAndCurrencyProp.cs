using Microsoft.EntityFrameworkCore.Migrations;

namespace MoldovaEchangeRateProcessor.ProcessorWorkerService.Migrations
{
    public partial class AddIndexConstraintToExchangeRateDatePropAndBankPropAndCurrencyProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_BankId",
                table: "ExchangeRates");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_BankId_Date_Currency",
                table: "ExchangeRates",
                columns: new[] { "BankId", "Date", "Currency" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_BankId_Date_Currency",
                table: "ExchangeRates");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "ExchangeRates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_BankId",
                table: "ExchangeRates",
                column: "BankId");
        }
    }
}
