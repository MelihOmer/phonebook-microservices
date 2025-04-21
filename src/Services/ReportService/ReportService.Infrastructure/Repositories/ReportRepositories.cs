using Microsoft.EntityFrameworkCore;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;

namespace ReportService.Infrastructure.Repositories
{
    public class ReportRepositories : IReportRepository
    {
        private readonly ReportDbContext _dbContext;
        public ReportRepositories(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private DbSet<Report> Table => _dbContext.Set<Report>();

        public async Task<Report> AddReportAsync(Report report)
        {
            var entityEntry = await Table.AddAsync(report);
            return entityEntry.Entity;

        }

        public async Task<int> CommitAsync()
        {
            var effectedRows = await _dbContext.SaveChangesAsync();
            return effectedRows;

        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            var result = await Table.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Report> GetReportByIdAsync(Guid reportId)
        {
            var result = await Table.FindAsync(reportId);
            return result;
        }

        public Report UpdateReport(Report report)
        {
            var entityEntry =  Table.Update(report);
            return entityEntry.Entity;
        }
    }
}
