using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    public partial class MakeupAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MakeupArtist_MakeupArtistID1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MakeupArtistID1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentID",
                table: "MakeupArtist");

            migrationBuilder.DropColumn(
                name: "MakeupArtistID1",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments",
                column: "MakeupArtistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentID",
                table: "MakeupArtist",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MakeupArtistID1",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments",
                column: "MakeupArtistID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MakeupArtistID1",
                table: "Appointments",
                column: "MakeupArtistID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MakeupArtist_MakeupArtistID1",
                table: "Appointments",
                column: "MakeupArtistID1",
                principalTable: "MakeupArtist",
                principalColumn: "ID");
        }
    }
}
