using Microsoft.EntityFrameworkCore;

namespace SoleMates.Models
{
    public class myContext : DbContext
    {
        public myContext(DbContextOptions<myContext>options) : base(options)
        {
            
        }
        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Customer> tbl_customer { get; set; }
        public DbSet<Category> tbl_category { get; set; }
        public DbSet<Product> tbl_product { get; set; }
        public DbSet<Cart> tbl_cart { get; set; }
        public DbSet<Feedback> tbl_feedback { get; set; }
        public DbSet<Faqs> tbl_faqs { get; set; }
        public DbSet<Order> tbl_order { get; set; }
        public DbSet<OrderDetail> tbl_orderdetail { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p=>p.Category)
                .WithMany(c=>c.Product)
                .HasForeignKey(p=>p.cat_id);

            modelBuilder.Entity<Order>()
            .ToTable("tbl_order")
            .HasKey(o => o.order_id);

            modelBuilder.Entity<OrderDetail>()
                .ToTable("tbl_orderdetail")
                .HasKey(od => od.orderdetail_id);

            modelBuilder.Entity<Order>()
                .Property(o => o.order_id)
                .HasColumnName("order_id");

            modelBuilder.Entity<Order>()
                .Property(o => o.customer_id)
                .HasColumnName("customer_id");

            modelBuilder.Entity<Order>()
                .Property(o => o.order_date)
                .HasColumnName("order_date");

            modelBuilder.Entity<Order>()
                .Property(o => o.total_amount)
                .HasColumnName("total_amount");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.orderdetail_id)
                .HasColumnName("orderdetail_id");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.order_id)
                .HasColumnName("order_id");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.product_id)
                .HasColumnName("product_id");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.quantity)
                .HasColumnName("quantity");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.price)
                .HasColumnName("unit_price");
        }
    }
}
