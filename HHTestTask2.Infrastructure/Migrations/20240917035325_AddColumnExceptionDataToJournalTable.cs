using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHTestTask2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnExceptionDataToJournalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExceptionData",
                table: "Journal",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionData",
                table: "Journal");
        }
    }
}
