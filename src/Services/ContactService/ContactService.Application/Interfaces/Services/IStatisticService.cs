using ContactService.Application.DTOs.Statistic;

namespace ContactService.Application.Interfaces.Services
{
    public interface IStatisticService
    {
        Task<IEnumerable<LocationStatisticDto>> GetLocationStatisticAsync();
    }
}
