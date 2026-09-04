using Microsoft.EntityFrameworkCore; // Imports EF Core capabilities
using DisasterAlleviationApp.Models; // Imports your Donation model namespace

namespace DisasterAlleviationApp.Data
{
    // Inheriting from DbContext gives this class database querying and saving powers
    public class ApplicationDbContext : DbContext
    {
        // Constructor that receives database configuration settings (like connection strings) from Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tells Entity Framework to create a table named "Donations" in SQL Server using the Donation model columns
        public DbSet<Donation> Donations { get; set; }
    }
}