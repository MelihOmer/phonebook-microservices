using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhonebookMicroservices.Shared.ResponseTypes;
using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Application.Interfaces.Services;

namespace ReportService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportDetailsController : ControllerBase
    {
        private readonly IReportDetailService _reportDetailService;

        public ReportDetailsController(IReportDetailService reportDetailService)
        {
            _reportDetailService = reportDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReportDetailsAsync()
        {
            var result = await _reportDetailService.GetAllReportDetailsAsync();
            var response = ApiResponse<IEnumerable<ReportDetailResponseDto>>.Ok(result);
            return Ok(response);
        }
        [HttpGet("{reportId:Guid}")]
        public async Task<IActionResult> GetReportDetailByIdAsync([FromRoute]Guid reportId)
        {
            var result = await _reportDetailService.GetReportDetailsByReportId(reportId);
            var response = ApiResponse<IEnumerable<ReportDetailResponseDto>>.Ok(result);
            return Ok(response);
        }
    }
}
