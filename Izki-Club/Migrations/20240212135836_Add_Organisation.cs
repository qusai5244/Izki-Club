using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Izki_Club.Migrations
{
    /// <inheritdoc />
    public partial class Add_Organisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "organisationId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "organisationId",
                table: "Referees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionEn = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_organisationId",
                table: "Teams",
                column: "organisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Referees_organisationId",
                table: "Referees",
                column: "organisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referees_Organizations_organisationId",
                table: "Referees",
                column: "organisationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organizations_organisationId",
                table: "Teams",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organizations_organisationId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Teams_organisationId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Referees_organisationId",
                table: "Referees");

            migrationBuilder.DropColumn(
                name: "organisationId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "organisationId",
                table: "Referees");
        }
    }
}
