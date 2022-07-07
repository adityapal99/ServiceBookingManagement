using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservice.Models
{
    public class Database : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Database(DbContextOptions<Database> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Database"));
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppProduct>()
                .Property(p => p.CreatedDate)
                .HasDefaultValueSql("getDate()");
            modelBuilder.Entity<AppProduct>()
                .ToTable("AppUser");
        }

        #nullable enable
        public DbSet<AppProduct> AppProducts { get; set; }
    }
}
