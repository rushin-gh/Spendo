using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<ExpenseModel> Expenses => Set<ExpenseModel>();
    }
}
