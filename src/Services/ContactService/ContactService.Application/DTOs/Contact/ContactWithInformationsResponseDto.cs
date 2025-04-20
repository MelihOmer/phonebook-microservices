using ContactService.Application.DTOs.ContactInformation;

namespace ContactService.Application.DTOs.Contact
{
    public record ContactWithInformationsResponseDto
    {
        public ContactResponseDto Contact { get; set; }
        public IEnumerable<ContactInfoListDto> ContactInformations{ get; set; }
       

    }
}
