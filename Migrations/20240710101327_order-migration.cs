using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoleMates.Migrations
{
    public partial class ordermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Customerscustomer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_tbl_order_tbl_customer_Customerscustomer_id",
                        column: x => x.Customerscustomer_id,
                        principalTable: "tbl_customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_orderdetail",
                columns: table => new
                {
                    orderdetail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ordersorder_id = table.Column<int>(type: "int", nullable: false),
                    Productsproduct_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_orderdetail", x => x.orderdetail_id);
                    table.ForeignKey(
                        name: "FK_tbl_orderdetail_tbl_order_Ordersorder_id",
                        column: x => x.Ordersorder_id,
                        principalTable: "tbl_order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_orderdetail_tbl_product_Productsproduct_id",
                        column: x => x.Productsproduct_id,
                        principalTable: "tbl_product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_Customerscustomer_id",
                table: "tbl_order",
                column: "Customerscustomer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderdetail_Ordersorder_id",
                table: "tbl_orderdetail",
                column: "Ordersorder_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderdetail_Productsproduct_id",
                table: "tbl_orderdetail",
                column: "Productsproduct_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_orderdetail");

            migrationBuilder.DropTable(
                name: "tbl_order");
        }
    }
}
