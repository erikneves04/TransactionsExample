using Microsoft.EntityFrameworkCore;
using TransactionsExample.Domain.Entities;

namespace TransactionsExample.Data.ContextDb;

public partial class Context : DbContext
{
    public DbSet<BankAccount> Accounts { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //const string connectionString = "Server=localhost,1433;Database=TransactionsExample;User ID=sa;Password=teste-123;TrustServerCertificate=True"; ;
        const string connectionString = "Integrated Security=SSPI;Persist Security Info=true;Data Source=ERIK;Initial Catalog=TransactionsExample;User ID=sa;Password=teste-123;TrustServerCertificate=True;";
        optionsBuilder
            .UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}