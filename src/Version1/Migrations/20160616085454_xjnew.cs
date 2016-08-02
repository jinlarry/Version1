using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Version1.Migrations
{
    public partial class xjnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    event_ID = table.Column<string>(nullable: false),
                    event_address = table.Column<string>(nullable: true),
                    event_datetime = table.Column<DateTime>(nullable: false),
                    event_name = table.Column<string>(nullable: true),
                    event_picture = table.Column<string>(nullable: true),
                    event_profile = table.Column<string>(nullable: true),
                    teamid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.event_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
