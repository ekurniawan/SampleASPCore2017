using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContohWeb.Migrations
{
    //dotnet ef migrations add InitialCreate
    //dotnet ef migrations add <add_column_address_table_students>
    //dotnet ef migrations remove <delete_column_address_table_students>
    //dotnet ef database update
    public partial class add_column_address_table_student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");
        }
    }
}
