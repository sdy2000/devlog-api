using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class DevLogDbContext : DbContext
{
    public DevLogDbContext()
    {
    }

    public DevLogDbContext(DbContextOptions<DevLogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CatTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Comment");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Comment1)
                .HasMaxLength(700)
                .HasColumnName("Comment");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Post");

            entity.HasOne(d => d.User).WithOne(p => p.Comment)
                .HasForeignKey<Comment>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PostShortDiscription).HasMaxLength(500);
            entity.Property(e => e.PostTitle).HasMaxLength(150);
            entity.Property(e => e.Tags).HasMaxLength(500);

            entity.HasOne(d => d.Author).WithMany(p => p.InverseAuthor)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_Post");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.UserAvatar).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
