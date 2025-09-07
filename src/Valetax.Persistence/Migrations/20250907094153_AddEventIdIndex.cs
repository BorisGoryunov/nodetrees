using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Valetax.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventIdIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Journal_EventId",
                table: "Journal",
                column: "EventId")
                .Annotation("Npgsql:IndexMethod", "hash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Journal_EventId",
                table: "Journal");
        }
    }
}
