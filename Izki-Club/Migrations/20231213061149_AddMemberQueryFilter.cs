using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Izki_Club.Migrations
{
    public partial class AddMemberQueryFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Members ADD CONSTRAINT CK_Member_IsDeleted CHECK (IsDeleted = 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Members DROP CONSTRAINT CK_Member_IsDeleted");
        }
    }
}
