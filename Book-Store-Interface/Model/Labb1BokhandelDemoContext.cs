using Microsoft.EntityFrameworkCore;

namespace Book_Store_Interface.Model;

public partial class Labb1BokhandelDemoContext : DbContext
{
    public Labb1BokhandelDemoContext()
    {
    }

    public Labb1BokhandelDemoContext(DbContextOptions<Labb1BokhandelDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BooksAuthor> BooksAuthors { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OutofStock> OutofStocks { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Initial Catalog=Labb1_BokhandelDemo;Integrated Security=True;Trust Server Certificate=True;Server SPN=localhost");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC27A15D1889");

            entity.ToTable("Author");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BirthDate).HasColumnName("birthDate");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.Initial)
                .HasMaxLength(10)
                .HasColumnName("initial");
            entity.Property(e => e.IsDead).HasColumnName("isDead");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Books__3BF79E033B99087E");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.LastChangeTime)
                .HasPrecision(1)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("lastChangeTime");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("productID");
            entity.Property(e => e.PublishYear).HasColumnName("publishYear");
            entity.Property(e => e.PublisherId)
                .HasDefaultValueSql("('1')")
                .HasColumnName("publisherID");
            entity.Property(e => e.Series).HasColumnName("series");
            entity.Property(e => e.SeriesPart).HasColumnName("seriesPart");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__publisher__4668671F");
        });

        modelBuilder.Entity<BooksAuthor>(entity =>
        {
            entity.HasKey(e => e.BooksAuthorsId).HasName("PK__BooksAut__E603778700C60DC6");

            entity.Property(e => e.BooksAuthorsId).HasColumnName("BooksAuthorsID");
            entity.Property(e => e.AuthorsId).HasColumnName("AuthorsID");
            entity.Property(e => e.BooksId)
                .HasMaxLength(13)
                .HasColumnName("BooksID");

            entity.HasOne(d => d.Authors).WithMany(p => p.BooksAuthors)
                .HasForeignKey(d => d.AuthorsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BooksAuth__Autho__4944D3CA");

            entity.HasOne(d => d.Books).WithMany(p => p.BooksAuthors)
                .HasForeignKey(d => d.BooksId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BooksAuth__Books__4A38F803");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB9DF1DBC132");

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
            entity.Property(e => e.PostalCode).HasColumnName("postalCode");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Isbn }).HasName("PK__Inventor__BAE0C55DFF5A1699");

            entity.ToTable("Inventory");

            entity.Property(e => e.StoreId).HasColumnName("storeID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");
            entity.Property(e => e.CurrentInventory)
                .HasDefaultValueSql("('0')")
                .HasColumnName("currentInventory");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__ISBN__50E5F592");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__store__4FF1D159");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809337D9B6F16A3");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("orderID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.SaleDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("saleDate");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__customer__569ECEE8");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_De__3214EC2738F76D81");

            entity.ToTable("Order_Details", tb => tb.HasTrigger("OD_InsertUpdate"));

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasDefaultValue("1")
                .HasColumnName("ID");
            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.OrderItem)
                .HasMaxLength(13)
                .HasColumnName("orderItem");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Det__order__1B48FEF0");

            entity.HasOne(d => d.OrderItemNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Det__order__1C3D2329");
        });

        modelBuilder.Entity<OutofStock>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OutofStock");

            entity.Property(e => e.Author).HasMaxLength(101);
            entity.Property(e => e.CurrentInventory).HasColumnName("Current Inventory");
            entity.Property(e => e.DaysOutOfStock)
                .HasMaxLength(30)
                .HasColumnName("Days out of Stock");
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");
            entity.Property(e => e.OrderFrom).HasColumnName("Order From");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("PK__Publishe__6B59A4AD1A349076");

            entity.ToTable("Publisher");

            entity.Property(e => e.PubId).HasColumnName("pubID");
            entity.Property(e => e.ContactEmail)
                .HasDefaultValue("book@unavailable.nu")
                .HasColumnName("contact_email");
            entity.Property(e => e.Name)
                .HasDefaultValue("Unavailable")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stores__3214EC2753F695B4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(50)
                .HasColumnName("contactEmail");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .HasColumnName("contactNumber");
            entity.Property(e => e.PostalCode).HasColumnName("postalCode");
            entity.Property(e => e.StoreName).HasColumnName("storeName");
        });

        modelBuilder.Entity<TitlesPerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlesPerAuthor");

            entity.Property(e => e.Age).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(101);
            entity.Property(e => e.TotalInventoryValue)
                .HasMaxLength(34)
                .HasColumnName("Total Inventory Value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
