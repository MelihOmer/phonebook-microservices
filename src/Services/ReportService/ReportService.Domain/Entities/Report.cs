using ReportService.Domain.Common;
using ReportService.Domain.Enums;

namespace ReportService.Domain.Entities
{
    public class Report : BaseEntity
    {
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public ReportStatus ReportStatus { get; set; } = ReportStatus.Preparing;
        public IEnumerable<ReportDetail> ReportDetails { get; set; }
    }
}
