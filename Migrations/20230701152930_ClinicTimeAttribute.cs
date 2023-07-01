using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Appointment.Migrations
{
    /// <inheritdoc />
    public partial class ClinicTimeAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endtime",
                table: "dailyAvailbilities");

            migrationBuilder.RenameColumn(
                name: "PhonNum",
                table: "Patients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Patients",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "Clinic_PhonNum",
                table: "Doctors",
                newName: "Clinic_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "dailyAvailbilities",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "starttime",
                table: "dailyAvailbilities",
                newName: "Clinic_Time");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UsersLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UsersLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Patients",
                newName: "PhonNum");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Patients",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Clinic_PhoneNumber",
                table: "Doctors",
                newName: "Clinic_PhonNum");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "dailyAvailbilities",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Clinic_Time",
                table: "dailyAvailbilities",
                newName: "starttime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UsersLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UsersLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "endtime",
                table: "dailyAvailbilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
