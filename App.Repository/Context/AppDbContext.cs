using App.Repository.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Context;

public partial class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<Product> Products { get; set; } // Representação dos Products na conta Stripe no banco local
    public virtual DbSet<Price> Prices { get; set; } // Representação dos Prices na conta Stripe no banco local

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

   
}


