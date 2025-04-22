namespace ReportService.Application.DTOs.StatisticDTOs
{
    public class LocationStatisticDto
    {
        public Guid ReportId { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
