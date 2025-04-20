using ContactService.Domain.Enums;

namespace ContactService.Application.DTOs.ContactInformation
{
    public record ContactInfoResponseDto
    {
        public Guid Id { get; init; }
        public Guid ContactId { get; init; }
        public ContactInfoType Type { get; init; }
        public string InfoContent { get; init; }
        public ContactInfoResponseDto(Guid id, Guid contactId, ContactInfoType type, string infoContent)
        {
            Id = id;
            ContactId = contactId;
            Type = type;
            InfoContent = infoContent;
        }

    }
}
