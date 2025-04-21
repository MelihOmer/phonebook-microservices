using ReportService.Domain.Entities;

namespace ReportService.Application.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<Report> GetReportByIdAsync(Guid reportId);
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report> AddReportAsync(Report report);
        Report UpdateReport(Report report);
        Task<int> CommitAsync();
    }
}
