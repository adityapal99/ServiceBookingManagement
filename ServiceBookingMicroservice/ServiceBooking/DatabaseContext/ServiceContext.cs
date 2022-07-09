using Microsoft.EntityFrameworkCore;
using ServiceBooking.Models;

namespace ServiceBooking.DatabaseContext
{
    public class ServiceContext:DbContext
    {
        public ServiceContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ServiceRequest> Requests { get; set; }
        public DbSet<ServiceReport> Reports { get; set; }
    }
}
