using ContactService.Domain.Common;
using ContactService.Domain.Enums;

namespace ContactService.Domain.Entities
{
    public class ContactInformation : BaseEntity
    {
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        public ContactInfoType Type { get; set; }
        public string InfoContent { get; set; }
    }
}
