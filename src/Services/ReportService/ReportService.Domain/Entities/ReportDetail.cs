using ReportService.Domain.Common;

namespace ReportService.Domain.Entities
{
    public class ReportDetail : BaseEntity
    {
        public Guid ReportId { get; set; }
        public Report Report { get; set; }

        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }

    }
}
