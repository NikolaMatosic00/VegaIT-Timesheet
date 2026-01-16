using Microsoft.EntityFrameworkCore;

namespace VegaIT.Timesheet.Persistence.Models.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=TimesheetDB;Trusted_Connection=True;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<SingleTask> SingleTasks { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }

        

    }
}



           // optionsBuilder.UseSqlServer("Server=.;Database=TimesheetDB;Trusted_Connection=True;");
   

    

