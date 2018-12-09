using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Library.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<BookCopies> BookCopies { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Lang> Lang { get; set; }
        public virtual DbSet<LibraryCard> LibraryCard { get; set; }
        public virtual DbSet<TakeBooks> TakeBooks { get; set; }
        public virtual DbSet<Ydk> Ydk { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TNBTOCU;Database=Library;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<BookCopies>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("book_copies");

                entity.Property(e => e.BookCode).HasColumnName("Book_code");

                entity.Property(e => e.Destroyed)
                    .HasColumnName("destroyed")
                    .HasMaxLength(1);

                entity.Property(e => e.Issued)
                    .HasColumnName("issued")
                    .HasMaxLength(1);

                entity.HasOne(d => d.BookCodeNavigation)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookCode)
                    .HasConstraintName("FK__book_copi__Book___33D4B598");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Annotation).HasColumnType("text");

                entity.Property(e => e.AuthorCode)
                    .HasColumnName("Author_code")
                    .HasMaxLength(15);

                entity.Property(e => e.Book).HasMaxLength(150);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasColumnName("Book_name")
                    .HasMaxLength(30);

                entity.Property(e => e.BookYear).HasColumnName("Book_Year");

                entity.Property(e => e.GenreCode)
                    .HasColumnName("Genre_code")
                    .HasMaxLength(15);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(30);

                entity.Property(e => e.LangCode)
                    .HasColumnName("Lang_code")
                    .HasMaxLength(2);

                entity.Property(e => e.Picture).HasMaxLength(150);

                entity.Property(e => e.YdkSectionCode)
                    .HasColumnName("YDK_Section_code")
                    .HasMaxLength(10);

                entity.HasOne(d => d.AuthorCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorCode)
                    .HasConstraintName("FK__Books__Author_co__2E1BDC42");

                entity.HasOne(d => d.GenreCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreCode)
                    .HasConstraintName("FK__Books__Genre_cod__2F10007B");

                entity.HasOne(d => d.LangCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.LangCode)
                    .HasConstraintName("FK__Books__Lang_code__30F848ED");

                entity.HasOne(d => d.YdkSectionCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.YdkSectionCode)
                    .HasConstraintName("FK__Books__YDK_Secti__300424B4");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(90);
            });

            modelBuilder.Entity<Lang>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .ValueGeneratedNever();

                entity.Property(e => e.Lang1)
                    .HasColumnName("Lang")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<LibraryCard>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.ToTable("Library_card");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Library___A9D1053434D357AA")
                    .IsUnique();

                entity.Property(e => e.Adress).HasMaxLength(30);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Telephone).HasMaxLength(15);
            });

            modelBuilder.Entity<TakeBooks>(entity =>
            {
                entity.HasKey(e => e.Numbr);

                entity.ToTable("Take_Books");

                entity.Property(e => e.Numbr)
                    .HasColumnName("numbr")
                    .ValueGeneratedNever();

                entity.Property(e => e.BookCode).HasColumnName("Book_code");

                entity.Property(e => e.DataBack)
                    .HasColumnName("Data_back")
                    .HasColumnType("date");

                entity.Property(e => e.DataTake)
                    .HasColumnName("Data_take")
                    .HasColumnType("date");

                entity.Property(e => e.LibraryCard).HasColumnName("Library_card");

                entity.Property(e => e.TimeToBack).HasColumnName("time_To_back");

                entity.HasOne(d => d.BookCodeNavigation)
                    .WithMany(p => p.TakeBooks)
                    .HasForeignKey(d => d.BookCode)
                    .HasConstraintName("FK__Take_Book__Book___398D8EEE");

                entity.HasOne(d => d.LibraryCardNavigation)
                    .WithMany(p => p.TakeBooks)
                    .HasForeignKey(d => d.LibraryCard)
                    .HasConstraintName("FK__Take_Book__Libra__38996AB5");
            });

            modelBuilder.Entity<Ydk>(entity =>
            {
                entity.HasKey(e => e.YdkCode);

                entity.ToTable("YDK");

                entity.Property(e => e.YdkCode)
                    .HasColumnName("YDK_code")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
