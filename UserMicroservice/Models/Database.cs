using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace UserMicroservice.Models
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("AppUser");
        }

        #nullable enable
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
