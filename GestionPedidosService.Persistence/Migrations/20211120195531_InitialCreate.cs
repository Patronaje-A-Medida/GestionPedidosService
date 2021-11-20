using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPedidosService.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeGarment = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NameGarment = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FirstRangePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SecondRangePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Available = table.Column<byte>(type: "tinyint", nullable: false),
                    AtelierId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeOrder = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtelierId = table.Column<int>(type: "int", nullable: false),
                    UserClientId = table.Column<int>(type: "int", nullable: false),
                    UserAtelierId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureGarment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeFeature = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    GarmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureGarment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureGarment_Garment_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternGarment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePattern = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ImagePattern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScaledStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    GarmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternGarment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternGarment_Garment_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(7)", nullable: false),
                    GarmentId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Garment_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternDimension",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    PatternGarmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternDimension", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternDimension_PatternGarment_PatternGarmentId",
                        column: x => x.PatternGarmentId,
                        principalTable: "PatternGarment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureGarment_GarmentId",
                table: "FeatureGarment",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_GarmentId",
                table: "OrderDetail",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternDimension_PatternGarmentId",
                table: "PatternDimension",
                column: "PatternGarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternGarment_GarmentId",
                table: "PatternGarment",
                column: "GarmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureGarment");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "PatternDimension");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "PatternGarment");

            migrationBuilder.DropTable(
                name: "Garment");
        }
    }
}
