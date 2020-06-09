using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meds_Server.Migrations
{
    public partial class Change01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Pieces = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 30, nullable: true),
                    Best_Before = table.Column<DateTime>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true),
                    Base_Substance = table.Column<string>(maxLength: 50, nullable: true),
                    Base_Substance_Quantity = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meds");
        }
    }
}
