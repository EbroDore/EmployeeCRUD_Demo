using EmployeeCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Data
{
    public class MVCDemoEbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public MVCDemoEbContext(DbContextOptions options) : base(options)
        {

        }



    }
}
