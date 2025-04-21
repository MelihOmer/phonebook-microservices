namespace ReportService.Application.DTOs.ReportDetailDTOs
{
    public record ReportDetailResponseDto
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
