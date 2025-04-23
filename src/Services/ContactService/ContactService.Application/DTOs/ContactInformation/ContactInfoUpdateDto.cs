using ContactService.Domain.Enums;

namespace ContactService.Application.DTOs.ContactInformation
{
    public record ContactInfoUpdateDto
    {
        public ContactInfoUpdateDto(Guid id, ContactInfoType type, string infoContent)
        {
            Id = id;
            Type = type;
            InfoContent = infoContent;
        }

        public Guid Id { get; set; }
        public ContactInfoType Type { get; set; }
        public string InfoContent { get; set; }
    }
}
