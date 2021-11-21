using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPedidosService.Persistence.Migrations
{
    public partial class AddAtelierEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atelier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAtelier = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RucAtelier = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DescriptionAtelier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atelier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_AtelierId",
                table: "Order",
                column: "AtelierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Atelier_AtelierId",
                table: "Order",
                column: "AtelierId",
                principalTable: "Atelier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Atelier_AtelierId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Atelier");

            migrationBuilder.DropIndex(
                name: "IX_Order_AtelierId",
                table: "Order");
        }
    }
}
