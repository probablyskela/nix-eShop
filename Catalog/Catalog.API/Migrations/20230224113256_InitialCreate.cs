using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_category_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_consumer_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_product_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_product_variant_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CatalogCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogConsumer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogConsumer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PictureFileName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogProduct_CatalogCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CatalogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogConsumerCatalogProduct",
                columns: table => new
                {
                    ConsumersId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogConsumerCatalogProduct", x => new { x.ConsumersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CatalogConsumerCatalogProduct_CatalogConsumer_ConsumersId",
                        column: x => x.ConsumersId,
                        principalTable: "CatalogConsumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogConsumerCatalogProduct_CatalogProduct_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "CatalogProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogProductVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableStock = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogProductVariant_CatalogProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CatalogProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogProductVariantPictures",
                columns: table => new
                {
                    ProductVariantId = table.Column<int>(type: "integer", nullable: false),
                    PictureFileName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogProductVariantPictures", x => new { x.ProductVariantId, x.PictureFileName });
                    table.ForeignKey(
                        name: "FK_CatalogProductVariantPictures_CatalogProductVariant_Product~",
                        column: x => x.ProductVariantId,
                        principalTable: "CatalogProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogConsumerCatalogProduct_ProductsId",
                table: "CatalogConsumerCatalogProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogProduct_CategoryId",
                table: "CatalogProduct",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogProductVariant_ProductId",
                table: "CatalogProductVariant",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogConsumerCatalogProduct");

            migrationBuilder.DropTable(
                name: "CatalogProductVariantPictures");

            migrationBuilder.DropTable(
                name: "CatalogConsumer");

            migrationBuilder.DropTable(
                name: "CatalogProductVariant");

            migrationBuilder.DropTable(
                name: "CatalogProduct");

            migrationBuilder.DropTable(
                name: "CatalogCategory");

            migrationBuilder.DropSequence(
                name: "catalog_category_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_consumer_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_product_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_product_variant_hilo");
        }
    }
}
