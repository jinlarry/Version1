using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Version1.Migrations
{
    public partial class Zerorabbish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZeroRabbishRoute",
                columns: table => new
                {
                    RouteID = table.Column<string>(nullable: false),
                    CreateUserID = table.Column<string>(nullable: false),
                    Createdate = table.Column<DateTime>(nullable: false),
                    PathColor = table.Column<string>(nullable: true),
                    RouteNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZeroRabbishRoute", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "ZeroRabbishRoutePoint",
                columns: table => new
                {
                    PointID = table.Column<string>(nullable: false),
                    PostalAddress = table.Column<string>(nullable: true),
                    RouteID = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    lat = table.Column<string>(nullable: true),
                    lon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZeroRabbishRoutePoint", x => x.PointID);
                    table.ForeignKey(
                        name: "FK_ZeroRabbishRoutePoint_ZeroRabbishRoute_RouteID",
                        column: x => x.RouteID,
                        principalTable: "ZeroRabbishRoute",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZeroRabbishRoutePoint_RouteID",
                table: "ZeroRabbishRoutePoint",
                column: "RouteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZeroRabbishRoutePoint");

            migrationBuilder.DropTable(
                name: "ZeroRabbishRoute");
        }
    }
}
