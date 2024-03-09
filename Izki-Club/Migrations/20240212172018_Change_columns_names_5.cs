using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Izki_Club.Migrations
{
    /// <inheritdoc />
    public partial class Change_columns_names_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organizations_organizationId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "Teams",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_organizationId",
                table: "Teams",
                newName: "IX_Teams_OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organizations_OrganizationId",
                table: "Teams",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organizations_OrganizationId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Teams",
                newName: "organizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_OrganizationId",
                table: "Teams",
                newName: "IX_Teams_organizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organizations_organizationId",
                table: "Teams",
                column: "organizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
