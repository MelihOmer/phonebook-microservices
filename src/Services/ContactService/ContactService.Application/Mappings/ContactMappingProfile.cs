using AutoMapper;
using ContactService.Domain.DTOs.Contact;
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
