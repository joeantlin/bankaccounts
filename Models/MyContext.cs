using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        
        // "users" table is represented by this DbSet "Users"
        public DbSet<User> Users {get;set;}
        public DbSet<Transaction> Transactions {get;set;}
    }
}