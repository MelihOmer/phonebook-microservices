namespace ReportService.Application.DTOs.ReportDetailDTOs
{
    public record ReportDetailCreateDto
    {
        public Guid ReportId { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
