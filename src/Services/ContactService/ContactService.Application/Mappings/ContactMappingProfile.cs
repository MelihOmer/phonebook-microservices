using AutoMapper;
using ContactService.Application.DTOs.Contact;
using ContactService.Domain.Entities;

namespace ContactService.Application.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<Contact,ContactCreateDto>().ReverseMap();
            CreateMap<Contact,ContactResponseDto>().ReverseMap();
        }
    }
}
