using Microsoft.EntityFrameworkCore;
using LocationDepartmentApi.Models;
namespace LocationDepartmentApi.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }


    }
}
