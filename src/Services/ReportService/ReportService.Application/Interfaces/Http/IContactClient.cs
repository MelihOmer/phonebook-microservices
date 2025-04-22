using ReportService.Application.DTOs.StatisticDTOs;

namespace ReportService.Application.Interfaces.Http
{
    public interface IContactClient
    {
        Task<IEnumerable<LocationStatisticDto>> GetLocationStatisticAsync();
    }
}
