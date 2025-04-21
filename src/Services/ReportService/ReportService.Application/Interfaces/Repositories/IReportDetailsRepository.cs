using ReportService.Domain.Entities;

namespace ReportService.Application.Interfaces.Repositories
{
    public interface IReportDetailsRepository
    {
        Task<IEnumerable<ReportDetail>> GetAllReportDetailsAsync();
        Task<ReportDetail> GetReportDetailByIdAsync(Guid id);
        Task<ReportDetail> AddReportDetailAsync(ReportDetail reportDetail);
        IQueryable<ReportDetail> GetReportDetailQueryable();
        Task<int> CommitAsync();
    }
}
