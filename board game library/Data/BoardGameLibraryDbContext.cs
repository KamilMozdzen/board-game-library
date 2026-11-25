using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using board_game_library.Models;

namespace board_game_library.Data;

public partial class BoardGameLibraryDbContext : DbContext
{
    public BoardGameLibraryDbContext(DbContextOptions<BoardGameLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameCopy> GameCopies { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<RentalItem> RentalItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BB302988F");

            entity.ToTable("Category");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8D821CEFB");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D105342D0E63FF").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1191E0222D");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534DFBD6BDE").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__2AB897FD18F152AE");

            entity.ToTable("Game");

            entity.Property(e => e.DailyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.Category).WithMany(p => p.Games)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Game_Category");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Games)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Game_Publisher");
        });

        modelBuilder.Entity<GameCopy>(entity =>
        {
            entity.HasKey(e => e.GameCopyId).HasName("PK__GameCopy__6A79E706A3560F65");

            entity.ToTable("GameCopy");

            entity.HasIndex(e => e.InventoryNumber, "UQ_GameCopy_InventoryNumber").IsUnique();

            entity.Property(e => e.ConditionDescription).HasMaxLength(500);
            entity.Property(e => e.InventoryNumber).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Game).WithMany(p => p.GameCopies)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameCopy_Game");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A3832B419DA");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(30);

            entity.HasOne(d => d.Rental).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Rental");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FABCC74CB0F");

            entity.ToTable("Publisher");

            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__Rental__9700594305EE5057");

            entity.ToTable("Rental");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rental_Customer");

            entity.HasOne(d => d.Employee).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rental_Employee");
        });

        modelBuilder.Entity<RentalItem>(entity =>
        {
            entity.HasKey(e => e.RentalItemId).HasName("PK__RentalIt__52E55E6FB32E98BC");

            entity.ToTable("RentalItem");

            entity.HasIndex(e => new { e.RentalId, e.GameCopyId }, "UQ_RentalItem_Rental_GameCopy").IsUnique();

            entity.HasIndex(e => e.GameCopyId, "UX_RentalItem_ActiveGameCopy")
                .IsUnique()
                .HasFilter("([ReturnDate] IS NULL)");

            entity.Property(e => e.DailyRate).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.GameCopy).WithOne(p => p.RentalItem)
                .HasForeignKey<RentalItem>(d => d.GameCopyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalItem_GameCopy");

            entity.HasOne(d => d.Rental).WithMany(p => p.RentalItems)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalItem_Rental");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
