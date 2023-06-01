using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class accountupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "Accounts",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "Accounts",
                newName: "Number");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Accounts",
                newName: "AccountType");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Accounts",
                newName: "AccountNumber");
        }
    }
}
