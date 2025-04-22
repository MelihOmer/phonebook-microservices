using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PhonebookMicroservices.Shared.ResponseTypes;
using ReportService.API.Contracts.Events;
using ReportService.Application.DTOs.ReportDTOs;
using ReportService.Application.Interfaces.Services;

namespace ReportService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReportsController(IReportService reportService, IPublishEndpoint publishEndpoint)
        {
            _reportService = reportService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var result = await _reportService.GetAllReportsAsync();
            var response = ApiResponse<IEnumerable<ReportResponseDto>>.Ok(result,"İşlem Başarılı.");
            return Ok(response);
        }
        [HttpGet("{reportId:Guid}")]
        public async Task<IActionResult> GetReportById(Guid reportId)
        {
            var result = await _reportService.GetReportByIdAsync(reportId);
            var response = ApiResponse<ReportResponseDto>.Ok(result);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReport()
        {
            var result = await _reportService.AddReportAsync();
            await _publishEndpoint.Publish(new ReportRequestEvent(result.Id));
            var response = ApiResponse<ReportResponseDto>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
    }
}
