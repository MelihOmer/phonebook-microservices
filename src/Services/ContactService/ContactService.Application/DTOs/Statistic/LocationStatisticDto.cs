namespace ContactService.Application.DTOs.Statistic
{
    public record LocationStatisticDto
    {
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
