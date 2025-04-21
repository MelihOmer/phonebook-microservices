using Microsoft.EntityFrameworkCore;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;

namespace ReportService.Infrastructure.Repositories.ReportDetailsRepositories
{
    public class ReportDetailRepositories : IReportDetailsRepository
    {
        private readonly ReportDbContext _dbContext;

        public ReportDetailRepositories(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private DbSet<ReportDetail> Table => _dbContext.Set<ReportDetail>();

        public async Task<ReportDetail> AddReportDetailAsync(ReportDetail reportDetail)
        {
            var entityEntry = await Table.AddAsync(reportDetail);
            return entityEntry.Entity;
        }

        public Task<int> CommitAsync()
        {
            var affectedRows = _dbContext.SaveChangesAsync();
            return affectedRows;
        }

        public async Task<IEnumerable<ReportDetail>> GetAllReportDetailsAsync()
        {
            var result = await Table.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<ReportDetail> GetReportDetailByIdAsync(Guid id)
        {
            var result = await Table.FindAsync(id);
            return result;
        }

        public IQueryable<ReportDetail> GetReportDetailQueryable()
        {
            return Table.AsQueryable();
        }
    }
}
