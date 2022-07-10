using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceBooking.Migrations
{
    public partial class ServiceRequestIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Requests_ServiceRequestId",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceRequestId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Requests_ServiceRequestId",
                table: "Reports",
                column: "ServiceRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Requests_ServiceRequestId",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceRequestId",
                table: "Reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Requests_ServiceRequestId",
                table: "Reports",
                column: "ServiceRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
