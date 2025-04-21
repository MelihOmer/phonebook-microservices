using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Persistence
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
            
        }
        public DbSet<Report> Reports{ get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }
    }
}
