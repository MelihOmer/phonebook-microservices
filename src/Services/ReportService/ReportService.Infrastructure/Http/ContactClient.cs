using ReportService.Application.DTOs.StatisticDTOs;
using ReportService.Application.Interfaces.Http;
using System.Net.Http.Json;

namespace ReportService.Infrastructure.Http
{
    public class ContactClient : IContactClient
    {
        private readonly HttpClient _httpClient;

        public ContactClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<LocationStatisticDto>> GetLocationStatisticAsync()
        {
            var response =
                await _httpClient
                .GetFromJsonAsync<IEnumerable<LocationStatisticDto>>("api/v1/statistics/location-statistic");
            return response ?? Enumerable.Empty<LocationStatisticDto>();
        }
    }
}
