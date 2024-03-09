using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Izki_Club.Migrations
{
    /// <inheritdoc />
    public partial class Change_columns_names_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {                  
            
            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "Teams",
                newName: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
