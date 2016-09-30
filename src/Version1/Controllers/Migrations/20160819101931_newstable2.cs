using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Version1.Migrations
{
    public partial class newstable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "AuthorID",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CensorTime",
                table: "News",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SensorID",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CensorTime",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SensorID",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "News",
                nullable: true);
        }
    }
}
