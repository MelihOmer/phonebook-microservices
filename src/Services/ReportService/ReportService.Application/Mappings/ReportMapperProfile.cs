using AutoMapper;
using ReportService.Application.DTOs.ReportDTOs;
using ReportService.Domain.Entities;

namespace ReportService.Application.Mappings
{
    public class ReportMapperProfile : Profile
    {
        public ReportMapperProfile()
        {
            CreateMap<Report,ReportResponseDto>().ReverseMap();
            CreateMap<Report,ReportUpdateDto>().ReverseMap();
        }
    }
}
