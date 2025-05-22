using ExecutionPca.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExecutionPca.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasNoKey();
            modelBuilder.Entity<Puesto>().HasNoKey();
            modelBuilder.Entity<Empleado>().HasNoKey();
            modelBuilder.Entity<Departamento>().HasNoKey(); 


            base.OnModelCreating(modelBuilder);
        }
    }
}
