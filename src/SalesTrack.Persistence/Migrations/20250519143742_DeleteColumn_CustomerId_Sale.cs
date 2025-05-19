using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesTrack.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumn_CustomerId_Sale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Sales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Sales",
                type: "uuid",
                nullable: true);
        }
    }
}
