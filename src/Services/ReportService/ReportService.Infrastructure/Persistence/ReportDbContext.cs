using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence.Configurations;

namespace ReportService.Infrastructure.Persistence
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
            
        }
        public DbSet<Report> Reports{ get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
