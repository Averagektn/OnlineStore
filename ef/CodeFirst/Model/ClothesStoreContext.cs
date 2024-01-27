using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Model;

public partial class ClothesStoreContext : DbContext
{
    public ClothesStoreContext()
    {
    }

    public ClothesStoreContext(DbContextOptions<ClothesStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProductVariant> OrderProductVariants { get; set; }

    public virtual DbSet<OrderTransaction> OrderTransactions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=clothes_store;Username=postgres;Password=password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("order_status", new[] { "accepted", "assembled", "shipped", "delivered" })
            .HasPostgresEnum("user_type", new[] { "customer", "admin" });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AdrId).HasName("pk_address");

            entity.ToTable("address");

            entity.Property(e => e.AdrId).HasColumnName("adr_id");
            entity.Property(e => e.AdrAddress)
                .HasMaxLength(255)
                .HasColumnName("adr_address");
            entity.Property(e => e.AdrPostcode)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("adr_postcode");
            entity.Property(e => e.AdrUser)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("adr_user");

            entity.HasOne(d => d.AdrUserNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AdrUser)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BraId).HasName("pk_brand");

            entity.ToTable("brand");

            entity.HasIndex(e => e.BraName, "uq_brand_name").IsUnique();

            entity.Property(e => e.BraId).HasColumnName("bra_id");
            entity.Property(e => e.BraName)
                .HasMaxLength(255)
                .HasColumnName("bra_name");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.CrtUser, e.CrtProductVariant }).HasName("pk_user_product");

            entity.ToTable("cart");

            entity.Property(e => e.CrtUser)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("crt_user");
            entity.Property(e => e.CrtProductVariant)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("crt_product_variant");
            entity.Property(e => e.CrtQuantity)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("crt_quantity");

            entity.HasOne(d => d.CrtProductVariantNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CrtProductVariant)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_product_variant");

            entity.HasOne(d => d.CrtUserNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CrtUser)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("pk_category");

            entity.ToTable("category");

            entity.HasIndex(e => e.CatName, "qu_category_name").IsUnique();

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.CatName)
                .HasMaxLength(255)
                .HasColumnName("cat_name");
            entity.Property(e => e.CatParent)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("cat_parent");

            entity.HasOne(d => d.CatParentNavigation).WithMany(p => p.InverseCatParentNavigation)
                .HasForeignKey(d => d.CatParent)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_categoty");

            entity.HasMany(d => d.SectionSecs).WithMany(p => p.CategoryCats)
                .UsingEntity<Dictionary<string, object>>(
                    "CategorySection",
                    r => r.HasOne<Section>().WithMany()
                        .HasForeignKey("SectionSecId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_section"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryCatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_category"),
                    j =>
                    {
                        j.HasKey("CategoryCatId", "SectionSecId").HasName("pk_category_section");
                        j.ToTable("category_section");
                        j.IndexerProperty<int>("CategoryCatId")
                            .ValueGeneratedOnAdd()
                            .UseIdentityAlwaysColumn()
                            .HasColumnName("category_cat_id");
                        j.IndexerProperty<int>("SectionSecId")
                            .ValueGeneratedOnAdd()
                            .UseIdentityAlwaysColumn()
                            .HasColumnName("section_sec_id");
                    });
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColId).HasName("pk_color");

            entity.ToTable("color");

            entity.HasIndex(e => e.ColName, "uq_color_name").IsUnique();

            entity.Property(e => e.ColId).HasColumnName("col_id");
            entity.Property(e => e.ColName)
                .HasMaxLength(50)
                .HasColumnName("col_name");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.MedId).HasName("pk_media");

            entity.ToTable("media");

            entity.HasIndex(e => e.MedFilename, "uq_filename_filetype").IsUnique();

            entity.Property(e => e.MedId).HasColumnName("med_id");
            entity.Property(e => e.MedBytes).HasColumnName("med_bytes");
            entity.Property(e => e.MedFilename)
                .HasMaxLength(100)
                .HasColumnName("med_filename");
            entity.Property(e => e.MedFiletype)
                .HasMaxLength(5)
                .HasColumnName("med_filetype");
            entity.Property(e => e.MedProduct)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("med_product");

            entity.HasOne(d => d.MedProductNavigation).WithMany(p => p.Media)
                .HasForeignKey(d => d.MedProduct)
                .HasConstraintName("fk_product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdId).HasName("pk_order");

            entity.ToTable("order");

            entity.Property(e => e.OrdId).HasColumnName("ord_id");
            entity.Property(e => e.OrdAddress)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("ord_address");
            entity.Property(e => e.OrdDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("ord_date");
            entity.Property(e => e.OrdPrice)
                .HasColumnType("money")
                .HasColumnName("ord_price");
            entity.Property(e => e.OrdUser)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("ord_user");

            entity.HasOne(d => d.OrdAddressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrdAddress)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_address");

            entity.HasOne(d => d.OrdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrdUser)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<OrderProductVariant>(entity =>
        {
            entity.HasKey(e => new { e.OpvOrder, e.OpvProductVariant }).HasName("pk_order_product_variant");

            entity.ToTable("order_product_variant");

            entity.Property(e => e.OpvOrder)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("opv_order");
            entity.Property(e => e.OpvProductVariant)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("opv_product_variant");
            entity.Property(e => e.OpvQuantity)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("opv_quantity");

            entity.HasOne(d => d.OpvOrderNavigation).WithMany(p => p.OrderProductVariants)
                .HasForeignKey(d => d.OpvOrder)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order");

            entity.HasOne(d => d.OpvProductVariantNavigation).WithMany(p => p.OrderProductVariants)
                .HasForeignKey(d => d.OpvProductVariant)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_product_variant");
        });

        modelBuilder.Entity<OrderTransaction>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("order_transactions");

            entity.Property(e => e.OrtId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ort_id");
            entity.Property(e => e.OrtUpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("ort_updated_at");

            entity.HasOne(d => d.Ort).WithMany()
                .HasForeignKey(d => d.OrtId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_order");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProId).HasName("pk_product");

            entity.ToTable("product");

            entity.HasIndex(e => new { e.ProBrand, e.ProCategory }, "id_brand_category");

            entity.HasIndex(e => e.ProName, "uq_product_name").IsUnique();

            entity.Property(e => e.ProId).HasColumnName("pro_id");
            entity.Property(e => e.ProAverageRating)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("pro_average_rating");
            entity.Property(e => e.ProBrand)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("pro_brand");
            entity.Property(e => e.ProCategory)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("pro_category");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .HasColumnName("pro_name");
            entity.Property(e => e.ProPrice)
                .HasColumnType("money")
                .HasColumnName("pro_price");

            entity.HasOne(d => d.ProBrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProBrand)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_brand");

            entity.HasOne(d => d.ProCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProCategory)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_category");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.PrvId).HasName("pk_product_variant");

            entity.ToTable("product_variant");

            entity.HasIndex(e => e.PrvSku, "uq_sku").IsUnique();

            entity.Property(e => e.PrvId).HasColumnName("prv_id");
            entity.Property(e => e.PrvColor)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("prv_color");
            entity.Property(e => e.PrvProduct)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("prv_product");
            entity.Property(e => e.PrvQuantity)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("prv_quantity");
            entity.Property(e => e.PrvSize)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("prv_size");
            entity.Property(e => e.PrvSku)
                .HasMaxLength(50)
                .HasColumnName("prv_sku");

            entity.HasOne(d => d.PrvColorNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.PrvColor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_color");

            entity.HasOne(d => d.PrvProductNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.PrvProduct)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_product");

            entity.HasOne(d => d.PrvSizeNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.PrvSize)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_size");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.RevId).HasName("pk_review");

            entity.ToTable("review");

            entity.HasIndex(e => new { e.RevProduct, e.RevRating }, "id_product_rating");

            entity.Property(e => e.RevId).HasColumnName("rev_id");
            entity.Property(e => e.RevAuthor)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("rev_author");
            entity.Property(e => e.RevComment).HasColumnName("rev_comment");
            entity.Property(e => e.RevDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("rev_date");
            entity.Property(e => e.RevProduct)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("rev_product");
            entity.Property(e => e.RevRating)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, null, 10L, null, null)
                .HasColumnName("rev_rating");
            entity.Property(e => e.RevTitle)
                .HasMaxLength(50)
                .HasColumnName("rev_title");

            entity.HasOne(d => d.RevAuthorNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RevAuthor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user");

            entity.HasOne(d => d.RevProductNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RevProduct)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_product");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SecId).HasName("pk_section");

            entity.ToTable("section");

            entity.HasIndex(e => e.SecName, "uq_section_name").IsUnique();

            entity.Property(e => e.SecId).HasColumnName("sec_id");
            entity.Property(e => e.SecName)
                .HasMaxLength(255)
                .HasColumnName("sec_name");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizId).HasName("pk_size");

            entity.ToTable("size");

            entity.HasIndex(e => e.SizName, "uq_size_name").IsUnique();

            entity.Property(e => e.SizId).HasColumnName("siz_id");
            entity.Property(e => e.SizName)
                .HasMaxLength(50)
                .HasColumnName("siz_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsrId).HasName("pk_user");

            entity.ToTable("user");

            entity.HasIndex(e => e.UsrEmail, "uq_email").IsUnique();

            entity.HasIndex(e => e.UsrPhone, "uq_phone").IsUnique();

            entity.Property(e => e.UsrId).HasColumnName("usr_id");
            entity.Property(e => e.UsrEmail)
                .HasMaxLength(254)
                .HasColumnName("usr_email");
            entity.Property(e => e.UsrFirstname)
                .HasMaxLength(50)
                .HasColumnName("usr_firstname");
            entity.Property(e => e.UsrLastname)
                .HasMaxLength(50)
                .HasColumnName("usr_lastname");
            entity.Property(e => e.UsrPassword)
                .HasMaxLength(128)
                .HasColumnName("usr_password");
            entity.Property(e => e.UsrPhone)
                .HasMaxLength(15)
                .HasColumnName("usr_phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
