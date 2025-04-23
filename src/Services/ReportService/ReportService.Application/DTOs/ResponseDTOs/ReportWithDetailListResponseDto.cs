using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Application.DTOs.ReportDTOs;

namespace ReportService.Application.DTOs.ResponseDTOs
{
    public class ReportWithDetailListResponseDto
    {
        public ReportResponseDto Report { get; set; }
        public IEnumerable<ReportDetailListResponseDto> ReportDetails { get; set; }
    }
}
