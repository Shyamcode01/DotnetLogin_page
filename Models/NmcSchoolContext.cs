using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Login_user.Models;

public partial class NmcSchoolContext : DbContext
{
    public NmcSchoolContext()
    {
    }

    public NmcSchoolContext(DbContextOptions<NmcSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserLogin>(entity =>
        {
            //entity
            //    .HasNoKey();
            entity  .ToTable("userLogin");

            entity.Property(e => e.Gmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gmail");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
