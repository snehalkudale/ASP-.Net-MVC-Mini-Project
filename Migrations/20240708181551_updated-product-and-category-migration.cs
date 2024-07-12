using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoleMates.Migrations
{
    public partial class updatedproductandcategorymigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "admin_Id",
                table: "tbl_admin",
                newName: "admin_id");

            migrationBuilder.AddColumn<int>(
                name: "cat_id",
                table: "tbl_product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_cat_id",
                table: "tbl_product",
                column: "cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_product_tbl_category_cat_id",
                table: "tbl_product",
                column: "cat_id",
                principalTable: "tbl_category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_product_tbl_category_cat_id",
                table: "tbl_product");

            migrationBuilder.DropIndex(
                name: "IX_tbl_product_cat_id",
                table: "tbl_product");

            migrationBuilder.DropColumn(
                name: "cat_id",
                table: "tbl_product");

            migrationBuilder.RenameColumn(
                name: "admin_id",
                table: "tbl_admin",
                newName: "admin_Id");

            
        }
    }
}
