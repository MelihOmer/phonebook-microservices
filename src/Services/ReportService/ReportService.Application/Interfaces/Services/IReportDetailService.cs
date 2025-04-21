using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Domain.Entities;

namespace ReportService.Application.Interfaces.Services
{
    public interface IReportDetailService
    {
        Task<IEnumerable<ReportDetailResponseDto>> GetAllReportDetailsAsync();
        Task<ReportDetailResponseDto> GetReportDetailByIdAsync(Guid id);
        Task<ReportDetailResponseDto> AddReportDetailAsync(ReportDetailCreateDto reportDetailCreateDto);
    }
}
