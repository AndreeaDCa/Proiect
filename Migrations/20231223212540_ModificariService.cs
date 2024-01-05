using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    public partial class ModificariService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceID",
                table: "MakeupArtist",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MakeupArtist_ServiceID",
                table: "MakeupArtist",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_MakeupArtist_Service_ServiceID",
                table: "MakeupArtist",
                column: "ServiceID",
                principalTable: "Service",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakeupArtist_Service_ServiceID",
                table: "MakeupArtist");

            migrationBuilder.DropIndex(
                name: "IX_MakeupArtist_ServiceID",
                table: "MakeupArtist");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "MakeupArtist");
        }
    }
}
