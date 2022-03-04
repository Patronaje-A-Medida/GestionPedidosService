using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPedidosService.Persistence.Migrations
{
    public partial class InitialGestionDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ParentType = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    AtelierId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeGarment = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NameGarment = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FirstRangePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SecondRangePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    AtelierId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeOrder = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    OrderStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AtelierId = table.Column<int>(type: "int", nullable: false),
                    UserClientId = table.Column<int>(type: "int", nullable: false),
                    UserAtelierId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_UserAteliers_UserAtelierId",
                        column: x => x.UserAtelierId,
                        principalTable: "UserAteliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_UserClients_UserClientId",
                        column: x => x.UserClientId,
                        principalTable: "UserClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureGarments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeFeature = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CodeFeature = table.Column<int>(type: "int", nullable: false),
                    GarmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureGarments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureGarments_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternGarments",
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
                    table.PrimaryKey("PK_PatternGarments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternGarments_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(7)", nullable: false),
                    Quantity = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderDetailStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    GarmentId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternDimensions",
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
                    table.PrimaryKey("PK_PatternDimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternDimensions_PatternGarments_PatternGarmentId",
                        column: x => x.PatternGarmentId,
                        principalTable: "PatternGarments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryTypes_AtelierId",
                table: "DictionaryTypes",
                column: "AtelierId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureGarments_GarmentId",
                table: "FeatureGarments",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_GarmentId",
                table: "OrderDetails",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserAtelierId",
                table: "Orders",
                column: "UserAtelierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserClientId",
                table: "Orders",
                column: "UserClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternDimensions_PatternGarmentId",
                table: "PatternDimensions",
                column: "PatternGarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternGarments_GarmentId",
                table: "PatternGarments",
                column: "GarmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryTypes");

            migrationBuilder.DropTable(
                name: "FeatureGarments");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PatternDimensions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PatternGarments");

            migrationBuilder.DropTable(
                name: "Garments");
        }
    }
}
