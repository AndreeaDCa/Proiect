using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    public partial class ModificariAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "MakeupArtistID",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments",
                column: "MakeupArtistID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "MakeupArtistID",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MakeupArtistID",
                table: "Appointments",
                column: "MakeupArtistID",
                unique: true,
                filter: "[MakeupArtistID] IS NOT NULL");
        }
    }
}
