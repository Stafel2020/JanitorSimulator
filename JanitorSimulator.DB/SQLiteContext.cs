using JanitorSimulator.Core;
using Microsoft.EntityFrameworkCore;

namespace JanitorSimulator.DB
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        // Переопределение метода
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDateBaseSQLite.db");
        }
}
}
