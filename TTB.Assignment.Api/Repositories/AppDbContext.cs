using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TTB.Assignment.API.Entities;

namespace TTB.Assignment.API.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<TransactionLog> TransactionLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ttb.assignment.db;Database=TTBAssignmentDB;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;User Id=sa;Password=P@ssw0rd;");
            //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TTBAssignmentDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
            //modelBuilder.Entity<Account>()
            //    .HasOne(a => a.User)
            //    .WithMany(u => u.Accounts)
            //    .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Transaction>()
        .HasOne(t => t.Account) // The primary account for this transaction
        .WithMany(a => a.Transactions) // One account can have many transactions
        .HasForeignKey(t => t.AccountId)
        .OnDelete(DeleteBehavior.Restrict); // Restrict cascade for AccountId

            
           modelBuilder.Entity<Transaction>()
               .HasOne(t => t.ReferenceAccount) // The reference account for this transaction
               .WithMany() // No reverse navigation from Account to Transactions
               .HasForeignKey(t => t.ReferenceAccountId)
               .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<TransactionLog>()
            //    .HasKey(a => a.LogId);

            //modelBuilder.Entity<TransactionLog>()
            //    .HasOne(a => a.User)
            //    .WithMany()
            //    .HasForeignKey(a => a.UserId);
        }
    }
}
