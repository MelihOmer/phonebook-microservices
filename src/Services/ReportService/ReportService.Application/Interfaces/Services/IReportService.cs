using ReportService.Application.DTOs.ReportDTOs;

namespace ReportService.Application.Interfaces.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportResponseDto>> GetAllReportsAsync();
        Task<ReportResponseDto> GetReportByIdAsync(Guid id);
        Task<ReportResponseDto> AddReportAsync();
        Task<ReportResponseDto> UpdateReportAsync(ReportUpdateDto reportUpdateDto);
        Task PrepareReportAsync(Guid reportId);
    }
}
