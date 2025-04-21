using ReportService.Domain.Enums;

namespace ReportService.Application.DTOs.ReportDTOs
{
    public record ReportResponseDto
    {
        public Guid Id { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
