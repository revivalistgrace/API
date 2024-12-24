using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expense { get; set; }
        public DbSet<Person> Person { get; set; }

        public DbSet<Income> Income { get; set; }
    } 
}
