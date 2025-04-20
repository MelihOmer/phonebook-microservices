using ContactService.Domain.Enums;

namespace ContactService.Application.DTOs.ContactInformation
{
    public record ContactInfoUpdateDto
    {
        public Guid Id { get; set; }
        public ContactInfoType Type { get; set; }
        public string InfoContent { get; set; }
    }
}
