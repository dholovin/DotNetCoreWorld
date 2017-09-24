using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace World.Migrations
{
    public partial class RemovedCommentsFromTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Trips",
                nullable: true);
        }
    }
}
