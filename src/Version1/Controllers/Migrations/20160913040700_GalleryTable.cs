using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Version1.Migrations
{
    public partial class GalleryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    PhotoID = table.Column<string>(nullable: false),
                    Album = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true),
                    PhotoSize = table.Column<float>(nullable: false),
                    PhotoTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.PhotoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gallery");
        }
    }
}
