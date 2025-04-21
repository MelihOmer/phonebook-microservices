using ReportService.Domain.Enums;

namespace ReportService.Application.DTOs.ReportDTOs
{
    public record ReportUpdateDto
    {
        public Guid Id { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
