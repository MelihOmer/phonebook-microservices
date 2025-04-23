using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Application.Interfaces.Services;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Services
{
    public class ReportDetailService : IReportDetailService
    {
        private readonly IReportDetailsRepository _repository;
        private readonly IMapper _mapper;

        public ReportDetailService(IReportDetailsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReportDetailResponseDto> AddReportDetailAsync(ReportDetailCreateDto reportDetailCreateDto)
        {
            var reportDetail = _mapper.Map<ReportDetail>(reportDetailCreateDto);
            var addedReportDetail = await _repository.AddReportDetailAsync(reportDetail);
            await _repository.CommitAsync();
            var responseDto = _mapper.Map<ReportDetailResponseDto>(addedReportDetail);
            return responseDto;
        }

        public async Task<IEnumerable<ReportDetailResponseDto>> GetAllReportDetailsAsync()
        {
            var result = await _repository.GetAllReportDetailsAsync();
            return _mapper.Map<IEnumerable<ReportDetailResponseDto>>(result);
        }

        public async Task<ReportDetailResponseDto> GetReportDetailByIdAsync(Guid id)
        {
            var result = await _repository.GetReportDetailByIdAsync(id);
            return _mapper.Map<ReportDetailResponseDto>(result);
        }

        public async Task<IEnumerable<ReportDetailResponseDto>> GetReportDetailsByReportId(Guid reportId)
        {
            var result =  _repository.GetReportDetailQueryable()
                .Where(x => x.ReportId.Equals(reportId))
                .AsEnumerable();
            return _mapper.Map<IEnumerable<ReportDetailResponseDto>>(result);
        }
    }
}
