using ContactService.Application.DTOs.Statistic;
using ContactService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using PhonebookMicroservices.Shared.ResponseTypes;

namespace ContactService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("location-statistic")]
        public async Task<IActionResult> GetLocationStatisticAsync()
        {
            var result = await _statisticService.GetLocationStatisticAsync();
            var response = ApiResponse<IEnumerable<LocationStatisticDto>>.Ok(result);
            return Ok(response);
        }
    }
}
