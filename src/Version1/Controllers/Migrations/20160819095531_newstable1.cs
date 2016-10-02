using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Version1.Migrations
{
    public partial class newstable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    NewsContent = table.Column<string>(nullable: true),
                    NewsImage = table.Column<string>(nullable: true),
                    NewsTitle = table.Column<string>(nullable: true),
                    NewsType = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
