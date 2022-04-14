using Employe.Models;
using Microsoft.EntityFrameworkCore;


namespace Employe.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<EmployeSecurity> EmployeSecurity { get; set; }
        public DbSet<EmployeSociete> EmployeSociete { get; set; }
       
    }
}
