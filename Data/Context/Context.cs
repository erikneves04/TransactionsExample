using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.ContextDb;

public partial class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
    : base(options)
    {
    }

    public DbSet<BankAccount> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}