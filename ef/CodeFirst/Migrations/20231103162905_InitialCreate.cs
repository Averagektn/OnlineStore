using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_status", "accepted,assembled,shipped,delivered")
                .Annotation("Npgsql:Enum:user_type", "customer,admin");

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    bra_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bra_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brand", x => x.bra_id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_parent = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cat_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.cat_id);
                    table.ForeignKey(
                        name: "fk_categoty",
                        column: x => x.cat_parent,
                        principalTable: "category",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "color",
                columns: table => new
                {
                    col_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    col_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_color", x => x.col_id);
                });

            migrationBuilder.CreateTable(
                name: "section",
                columns: table => new
                {
                    sec_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sec_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_section", x => x.sec_id);
                });

            migrationBuilder.CreateTable(
                name: "size",
                columns: table => new
                {
                    siz_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    siz_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_size", x => x.siz_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usr_email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    usr_password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    usr_phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    usr_firstname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    usr_lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.usr_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pro_brand = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    pro_category = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    pro_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pro_price = table.Column<decimal>(type: "money", nullable: false),
                    pro_average_rating = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.pro_id);
                    table.ForeignKey(
                        name: "fk_brand",
                        column: x => x.pro_brand,
                        principalTable: "brand",
                        principalColumn: "bra_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_category",
                        column: x => x.pro_category,
                        principalTable: "category",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category_section",
                columns: table => new
                {
                    category_cat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    section_sec_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_section", x => new { x.category_cat_id, x.section_sec_id });
                    table.ForeignKey(
                        name: "fk_category",
                        column: x => x.category_cat_id,
                        principalTable: "category",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_section",
                        column: x => x.section_sec_id,
                        principalTable: "section",
                        principalColumn: "sec_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    adr_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    adr_postcode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.adr_id);
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.adr_user,
                        principalTable: "user",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    med_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    med_product = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    med_bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    med_filetype = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    med_filename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media", x => x.med_id);
                    table.ForeignKey(
                        name: "fk_product",
                        column: x => x.med_product,
                        principalTable: "product",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_variant",
                columns: table => new
                {
                    prv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prv_color = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    prv_size = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    prv_product = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    prv_quantity = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'', '1', '0', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    prv_sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variant", x => x.prv_id);
                    table.ForeignKey(
                        name: "fk_color",
                        column: x => x.prv_color,
                        principalTable: "color",
                        principalColumn: "col_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_product",
                        column: x => x.prv_product,
                        principalTable: "product",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_size",
                        column: x => x.prv_size,
                        principalTable: "size",
                        principalColumn: "siz_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    rev_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rev_product = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    rev_author = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    rev_comment = table.Column<string>(type: "text", nullable: false),
                    rev_rating = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'', '1', '', '10', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    rev_title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rev_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_review", x => x.rev_id);
                    table.ForeignKey(
                        name: "fk_product",
                        column: x => x.rev_product,
                        principalTable: "product",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.rev_author,
                        principalTable: "user",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    ord_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ord_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ord_address = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ord_price = table.Column<decimal>(type: "money", nullable: false),
                    ord_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.ord_id);
                    table.ForeignKey(
                        name: "fk_address",
                        column: x => x.ord_address,
                        principalTable: "address",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.ord_user,
                        principalTable: "user",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    crt_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    crt_product_variant = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    crt_quantity = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_product", x => new { x.crt_user, x.crt_product_variant });
                    table.ForeignKey(
                        name: "fk_product_variant",
                        column: x => x.crt_product_variant,
                        principalTable: "product_variant",
                        principalColumn: "prv_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.crt_user,
                        principalTable: "user",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_product_variant",
                columns: table => new
                {
                    opv_order = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    opv_product_variant = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    opv_quantity = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_product_variant", x => new { x.opv_order, x.opv_product_variant });
                    table.ForeignKey(
                        name: "fk_order",
                        column: x => x.opv_order,
                        principalTable: "order",
                        principalColumn: "ord_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_product_variant",
                        column: x => x.opv_product_variant,
                        principalTable: "product_variant",
                        principalColumn: "prv_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_transactions",
                columns: table => new
                {
                    ort_id = table.Column<int>(type: "integer", nullable: false),
                    ort_updated_at = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_order",
                        column: x => x.ort_id,
                        principalTable: "order",
                        principalColumn: "ord_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_adr_user",
                table: "address",
                column: "adr_user");

            migrationBuilder.CreateIndex(
                name: "uq_brand_name",
                table: "brand",
                column: "bra_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_crt_product_variant",
                table: "cart",
                column: "crt_product_variant");

            migrationBuilder.CreateIndex(
                name: "IX_category_cat_parent",
                table: "category",
                column: "cat_parent");

            migrationBuilder.CreateIndex(
                name: "qu_category_name",
                table: "category",
                column: "cat_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_section_section_sec_id",
                table: "category_section",
                column: "section_sec_id");

            migrationBuilder.CreateIndex(
                name: "uq_color_name",
                table: "color",
                column: "col_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_media_med_product",
                table: "media",
                column: "med_product");

            migrationBuilder.CreateIndex(
                name: "uq_filename_filetype",
                table: "media",
                column: "med_filename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_ord_address",
                table: "order",
                column: "ord_address");

            migrationBuilder.CreateIndex(
                name: "IX_order_ord_user",
                table: "order",
                column: "ord_user");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_variant_opv_product_variant",
                table: "order_product_variant",
                column: "opv_product_variant");

            migrationBuilder.CreateIndex(
                name: "IX_order_transactions_ort_id",
                table: "order_transactions",
                column: "ort_id");

            migrationBuilder.CreateIndex(
                name: "id_brand_category",
                table: "product",
                columns: new[] { "pro_brand", "pro_category" });

            migrationBuilder.CreateIndex(
                name: "IX_product_pro_category",
                table: "product",
                column: "pro_category");

            migrationBuilder.CreateIndex(
                name: "uq_product_name",
                table: "product",
                column: "pro_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_variant_prv_color",
                table: "product_variant",
                column: "prv_color");

            migrationBuilder.CreateIndex(
                name: "IX_product_variant_prv_product",
                table: "product_variant",
                column: "prv_product");

            migrationBuilder.CreateIndex(
                name: "IX_product_variant_prv_size",
                table: "product_variant",
                column: "prv_size");

            migrationBuilder.CreateIndex(
                name: "uq_sku",
                table: "product_variant",
                column: "prv_sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_product_rating",
                table: "review",
                columns: new[] { "rev_product", "rev_rating" });

            migrationBuilder.CreateIndex(
                name: "IX_review_rev_author",
                table: "review",
                column: "rev_author");

            migrationBuilder.CreateIndex(
                name: "uq_section_name",
                table: "section",
                column: "sec_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_size_name",
                table: "size",
                column: "siz_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_email",
                table: "user",
                column: "usr_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_phone",
                table: "user",
                column: "usr_phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "category_section");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "order_product_variant");

            migrationBuilder.DropTable(
                name: "order_transactions");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "section");

            migrationBuilder.DropTable(
                name: "product_variant");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "size");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
