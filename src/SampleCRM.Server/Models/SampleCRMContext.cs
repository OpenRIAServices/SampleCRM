using Microsoft.EntityFrameworkCore;
#nullable enable

namespace SampleCRM.Web.Models;

public partial class SampleCRMContext : DbContext
{
    public SampleCRMContext()
    {
    }

    public SampleCRMContext(DbContextOptions<SampleCRMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<CountryCodes> CountryCodes { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<DbVersion> DbVersions { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<OrderItems> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatus { get; set; }

    public virtual DbSet<PaymentTypes> PaymentTypes { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Shippers> Shippers { get; set; }

    public virtual DbSet<TaxTypes> TaxTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=database\\SampleCRM.db;Mode=ReadOnly;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.Property(e => e.CategoryID).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.Property(e => e.CustomerID).ValueGeneratedNever();
        });

        modelBuilder.Entity<DbVersion>(entity =>
        {
            entity.HasKey(e => e.Version);

            entity.ToTable("DbVersion");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasIndex(e => e.CustomerID, "IX_Orders_CustomerID");

            entity.Property(e => e.OrderID).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerID);
        });

        modelBuilder.Entity<OrderItems>(entity =>
        {
            entity.HasKey(e => new { e.OrderID, e.OrderLine });

            entity.HasIndex(e => e.ProductID, "IX_OrderItems_ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasForeignKey(d => d.OrderID);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems).HasForeignKey(d => d.ProductID);
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Status);

            entity.ToTable("OrderStatus");

            entity.Property(e => e.Status).ValueGeneratedNever();
        });

        modelBuilder.Entity<PaymentTypes>(entity =>
        {
            entity.Property(e => e.PaymentTypeID).ValueGeneratedNever();
        });

        modelBuilder.Entity<Shippers>(entity =>
        {
            entity.Property(e => e.ShipperID).ValueGeneratedNever();
        });

        modelBuilder.Entity<TaxTypes>(entity =>
        {
            entity.Property(e => e.TaxTypeID).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
