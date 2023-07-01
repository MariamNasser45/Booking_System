using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Appointment.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }, // Columns in table
            values: new object[] { Guid.NewGuid().ToString(), "Patient", "Patient".ToUpper(), Guid.NewGuid().ToString() }
        );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Doctor", "Doctor".ToUpper(), Guid.NewGuid().ToString() }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Roles]");
        }
    }
}
