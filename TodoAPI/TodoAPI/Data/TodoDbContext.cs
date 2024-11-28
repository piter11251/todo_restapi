using Microsoft.EntityFrameworkCore;
using TodoAPI.Entities;

namespace TodoAPI.Data
{
    public class TodoDbContext: DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=tododb; user=root;password=admin123",
                new MySqlServerVersion(new Version(9,1,0)));
        }
    }
}
