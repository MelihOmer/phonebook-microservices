using ContactService.Domain.Enums;

namespace ContactService.Application.DTOs.ContactInformation
{
    public record ContactInfoCreateDto
    {
        public Guid ContactId { get; init; }
        public ContactInfoType Type { get; init; }
        public string InfoContent { get; init; }
        public ContactInfoCreateDto(Guid contactId, ContactInfoType type, string infoContent)
        {
            ContactId = contactId;
            Type = type;
            InfoContent = infoContent;
        }

    }
}
