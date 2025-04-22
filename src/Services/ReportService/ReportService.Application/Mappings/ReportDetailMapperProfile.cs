using AutoMapper;
using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Domain.Entities;

namespace ReportService.Application.Mappings
{
    public class ReportDetailMapperProfile : Profile
    {
        public ReportDetailMapperProfile()
        {
            CreateMap<ReportDetail,ReportDetailResponseDto>().ReverseMap();
        }
    }
}
