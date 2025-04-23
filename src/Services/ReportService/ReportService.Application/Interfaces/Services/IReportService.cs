using ReportService.Application.DTOs.ReportDTOs;
using ReportService.Application.DTOs.ResponseDTOs;

namespace ReportService.Application.Interfaces.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportResponseDto>> GetAllReportsAsync();
        Task<ReportResponseDto> GetReportByIdAsync(Guid id);
        Task<ReportWithDetailListResponseDto> GetReportWithDetailListByReportIdAsync(Guid reportId);
        Task<IEnumerable<ReportWithDetailListResponseDto>> GetReportWithDetailListByAsync();
        Task<ReportResponseDto> AddReportAsync();
        Task<ReportResponseDto> UpdateReportAsync(ReportUpdateDto reportUpdateDto);
        Task PrepareReportAsync(Guid reportId);
    }
}
