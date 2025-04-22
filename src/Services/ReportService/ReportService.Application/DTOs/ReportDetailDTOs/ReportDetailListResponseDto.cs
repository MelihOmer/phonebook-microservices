namespace ReportService.Application.DTOs.ReportDetailDTOs
{
    public class ReportDetailListResponseDto
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
