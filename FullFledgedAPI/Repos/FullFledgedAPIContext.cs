using System;
using System.Collections.Generic;
using FullFledgedAPI.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace FullFledgedAPI.Repos;

public partial class FullFledgedAPIContext : DbContext
{
    public FullFledgedAPIContext()
    {
    }

    public FullFledgedAPIContext(DbContextOptions<FullFledgedAPIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
