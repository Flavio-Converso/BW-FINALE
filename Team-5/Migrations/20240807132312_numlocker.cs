using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team_5.Migrations
{
    /// <inheritdoc />
    public partial class numlocker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumLocker",
                table: "Lockers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumLocker",
                table: "Lockers");
        }
    }
}
