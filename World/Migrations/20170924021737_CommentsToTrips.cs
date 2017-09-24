using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace World.Migrations
{
    public partial class CommentsToTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Trips");
        }
    }
}
