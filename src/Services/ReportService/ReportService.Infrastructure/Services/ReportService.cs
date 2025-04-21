using AutoMapper;
using ReportService.Application.DTOs.ReportDTOs;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Application.Interfaces.Services;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<ReportResponseDto> AddReportAsync()
        {
            var report = new Report();
            var addedReport = await _reportRepository.AddReportAsync(report);
            await _reportRepository.CommitAsync();
            var responseDto = _mapper.Map<ReportResponseDto>(addedReport);
            return responseDto;
        }

        public async Task<IEnumerable<ReportResponseDto>> GetAllReportsAsync()
        {
            var result = await _reportRepository.GetAllReportsAsync();
            var responseDto = _mapper.Map<IEnumerable<ReportResponseDto>>(result);
            return responseDto;
        }

        public async Task<ReportResponseDto> GetReportByIdAsync(Guid id)
        {
            var result = await _reportRepository.GetReportByIdAsync(id);
            var responseDto = _mapper.Map<ReportResponseDto>(result); ;
            return responseDto;
        }

        public async Task<ReportResponseDto> UpdateReportAsync(ReportUpdateDto reportUpdateDto)
        {
            var mappingReport = _mapper.Map<Report>(reportUpdateDto);
            var updatedReport = _reportRepository.UpdateReport(mappingReport);
            await _reportRepository.CommitAsync();
            var responseDto = _mapper.Map<ReportResponseDto>(updatedReport);
            return responseDto;
        }
    }
}
