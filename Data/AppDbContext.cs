using Microsoft.EntityFrameworkCore;

namespace ExecutionPca.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // No necesitamos DbSet porque usamos procedimientos almacenados con ADO.NET
    }
}
