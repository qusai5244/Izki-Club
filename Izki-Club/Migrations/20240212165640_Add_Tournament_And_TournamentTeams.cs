using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Izki_Club.Migrations
{
    /// <inheritdoc />
    public partial class Add_Tournament_And_TournamentTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referees_Organizations_organizationId",
                table: "Referees");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "Referees",
                newName: "organisationId");

            migrationBuilder.RenameIndex(
                name: "IX_Referees_organizationId",
                table: "Referees",
                newName: "IX_Referees_organisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referees_Organizations_organisationId",
                table: "Referees",
                column: "organisationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referees_Organizations_organisationId",
                table: "Referees");

            migrationBuilder.RenameColumn(
                name: "organisationId",
                table: "Referees",
                newName: "organizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Referees_organisationId",
                table: "Referees",
                newName: "IX_Referees_organizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referees_Organizations_organizationId",
                table: "Referees",
                column: "organizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
