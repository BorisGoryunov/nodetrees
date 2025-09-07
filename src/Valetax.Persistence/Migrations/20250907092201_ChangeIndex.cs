using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Valetax.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Node_TreeId_ParentId",
                table: "Node");

            migrationBuilder.CreateIndex(
                name: "IX_Node_TreeId_Id",
                table: "Node",
                columns: new[] { "TreeId", "Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Node_TreeId_Id",
                table: "Node");

            migrationBuilder.CreateIndex(
                name: "IX_Node_TreeId_ParentId",
                table: "Node",
                columns: new[] { "TreeId", "ParentId" },
                unique: true);
        }
    }
}
