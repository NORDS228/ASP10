using Microsoft.EntityFrameworkCore;

namespace API1.Models;

public partial class TeaShopContext : DbContext
{
    public TeaShopContext(DbContextOptions<TeaShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductReservation> ProductReservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Carts__701794906650D7BF");

            entity.HasIndex(e => e.UserId, "UQ__Carts__B9BF3306EDEE15D9").IsUnique();

            entity.Property(e => e.IdCart).HasColumnName("ID_cart");
            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasConversion(
                    value => value.ToDateTime(TimeOnly.MinValue),
                    value => DateOnly.FromDateTime(value));
            entity.Property(e => e.UserId).HasColumnName("user_ID");

            entity.HasOne(d => d.User).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carts__user_ID__45F365D3");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.IdCartItem).HasName("PK__CartItem__2D213196E7F7D584");

            entity.Property(e => e.IdCartItem).HasColumnName("ID_cart_item");
            entity.Property(e => e.CartId).HasColumnName("cart_ID");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItems__cart___49C3F6B7");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItems__produ__4AB81AF0");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Categori__AC5CA40EE2F8C902");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__5189E25510F63D78").IsUnique();

            entity.Property(e => e.IdCategory).HasColumnName("ID_category");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__89BFCD5916EBAC53");

            entity.Property(e => e.IdOrder).HasColumnName("ID_order");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.OrderDate)
                .HasColumnName("order_date")
                .HasColumnType("datetime")
                .HasConversion(
                    value => value.ToDateTime(TimeOnly.MinValue),
                    value => DateOnly.FromDateTime(value));
            entity.Property(e => e.StatusId).HasColumnName("status_ID");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UserId).HasColumnName("user_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__status_I__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__user_ID__5070F446");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.IdOrderItem).HasName("PK__OrderIte__5B1127FFE899946C");

            entity.Property(e => e.IdOrderItem).HasColumnName("ID_order_item");
            entity.Property(e => e.OrderId).HasColumnName("order_ID");
            entity.Property(e => e.PriceAtTime)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_at_time");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__order__5535A963");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__produ__5629CD9C");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.HasKey(e => e.IdStatus).HasName("PK__OrderSta__449BD7E669E51C10");

            entity.HasIndex(e => e.StatusName, "UQ__OrderSta__501B3753819BABD1").IsUnique();

            entity.Property(e => e.IdStatus).HasColumnName("ID_status");
            entity.Property(e => e.StatusName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Products__FD7FEC233E945E53");

            entity.Property(e => e.IdProduct).HasColumnName("ID_product");
            entity.Property(e => e.CategoryId).HasColumnName("category_ID");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("created_at")
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_path");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("size");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__catego__4222D4EF");
        });

        modelBuilder.Entity<ProductReservation>(entity =>
        {
            entity.ToTable("ProductReservations");
            entity.HasKey(reservation => reservation.IdReservation);
            entity.Property(reservation => reservation.IdReservation).HasColumnName("ID_reservation");
            entity.Property(reservation => reservation.ProductId).HasColumnName("product_ID");
            entity.Property(reservation => reservation.UserId).HasColumnName("user_ID");
            entity.Property(reservation => reservation.Quantity).HasColumnName("quantity");
            entity.Property(reservation => reservation.ReservedAt)
                .HasColumnName("reserved_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("sysutcdatetime()");
            entity.Property(reservation => reservation.CancelledAt)
                .HasColumnName("cancelled_at")
                .HasColumnType("datetime2");

            entity.HasOne(reservation => reservation.Product)
                .WithMany(product => product.Reservations)
                .HasForeignKey(reservation => reservation.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(reservation => reservation.User)
                .WithMany(user => user.Reservations)
                .HasForeignKey(reservation => reservation.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__45DFFBDB8C77B643");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__783254B1918884D6").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("ID_role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__D7B4671E6B623E19");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616465221726").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5721BE495D5").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_user");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RegistrationDate)
                .HasColumnName("registration_date")
                .HasColumnType("datetime")
                .HasConversion(
                    value => value.ToDateTime(TimeOnly.MinValue),
                    value => DateOnly.FromDateTime(value));
            entity.Property(e => e.RoleId).HasColumnName("role_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_ID__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
