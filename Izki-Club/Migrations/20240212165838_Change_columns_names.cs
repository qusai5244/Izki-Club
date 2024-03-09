using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Izki_Club.Migrations
{
    /// <inheritdoc />
    public partial class Change_columns_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tournamentStatus",
                table: "Tournaments",
                newName: "TournamentStatus");            
            
            migrationBuilder.RenameColumn(
                name: "organisationId",
                table: "Referees",
                newName: "organizationId");            
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
