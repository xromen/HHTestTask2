using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHTestTask2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeParentToChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_ParentNodeId",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "ParentNodeId",
                table: "Nodes",
                newName: "NodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Nodes_ParentNodeId",
                table: "Nodes",
                newName: "IX_Nodes_NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_NodeId",
                table: "Nodes",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_NodeId",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "NodeId",
                table: "Nodes",
                newName: "ParentNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Nodes_NodeId",
                table: "Nodes",
                newName: "IX_Nodes_ParentNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_ParentNodeId",
                table: "Nodes",
                column: "ParentNodeId",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}
