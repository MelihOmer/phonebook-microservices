using ContactService.Domain.Enums;

namespace ContactService.Application.DTOs.ContactInformation
{
    public class ContactInfoListDto
    {
        public ContactInfoListDto(Guid id, ContactInfoType type, string ınfoContent)
        {
            Id = id;
            Type = type;
            InfoContent = ınfoContent;
        }

        public Guid Id { get; init; }
        public ContactInfoType Type { get; init; }
        public string InfoContent { get; init; }
    }
}
