namespace ReportService.API.Contracts.Events
{
    public class ReportRequestEvent
    {
        public ReportRequestEvent(Guid reportId)
        {
            ReportId = reportId;
        }
        public Guid ReportId { get; set; }
    }
}
