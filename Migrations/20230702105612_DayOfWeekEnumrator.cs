using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Appointment.Migrations
{
    /// <inheritdoc />
    public partial class DayOfWeekEnumrator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "dailyAvailbilities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "dailyAvailbilities");
        }
    }
}
