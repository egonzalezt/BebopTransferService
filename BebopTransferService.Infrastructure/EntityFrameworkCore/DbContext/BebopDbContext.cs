namespace BebopTransferService.Infrastructure.EntityFrameworkCore.DbContext;

using ModelBuilder;
using Domain.File.Entities;
using Domain.Transfer.Entities;
using Microsoft.EntityFrameworkCore;

public class BebopDbContext : DbContext
{
    public DbSet<File> Files { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    public BebopDbContext(DbContextOptions<BebopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>().Configure();
        modelBuilder.Entity<Transfer>().Configure();
    }
}
