using DotnetConsoleTemplate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotnetConsoleTemplate.DataAccess
{
    /// <summary>
    /// GenerDbContext
    /// Pass the GenerDbContext configuration to the DbContext
    /// </summary>
    
    public class GenerDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// The ApplicationDbContext class must expose a public constructor with a DbContextOptions parameter.
        /// </summary>
        /// <param name="options"></param>
        public GenerDbContext(DbContextOptions<GenerDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options =>
            {
                options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}