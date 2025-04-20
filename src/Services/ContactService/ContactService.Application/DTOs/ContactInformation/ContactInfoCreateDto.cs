using ContactService.Domain.Enums;

namespace ContactService.Application.DTOs.ContactInformation
{
    public class ContactInfoCreateDto
    {
        public Guid ContactId { get; set; }
        public ContactInfoType Type { get; set; }
        public string InfoContent { get; set; }
    }
}
