using ContactService.Domain.Common;

namespace ContactService.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
        public IEnumerable<ContactInformation> ContactInformations { get; set; }
    }
}
