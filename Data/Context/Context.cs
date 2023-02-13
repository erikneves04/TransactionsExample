using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.ContextDb;

public partial class Context : DbContext
{
    public DbSet<BankAccount> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TransactionInMemoryDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}