using AutoMapper;
using ContactService.Application.DTOs.ContactInformation;
using ContactService.Domain.Entities;

namespace ContactService.Application.Mappings
{
    public class ContactInformationMappingProfile : Profile
    {
        public ContactInformationMappingProfile()
        {
            CreateMap<ContactInformation,ContactInfoCreateDto>().ReverseMap();
            CreateMap<ContactInformation,ContactInfoResponseDto>().ReverseMap();
        }
    }
}
