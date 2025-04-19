using ContactService.Domain.DTOs.Contact;

namespace ContactService.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<ContactResponseDto> CreateContactAsync(ContactCreateDto contactCreateDto);
        Task<ContactResponseDto> GetContactByIdAsync(Guid id);
        Task<IEnumerable<ContactResponseDto>> GetContactsByCompanyNameAsync(string companyName);
        Task<IEnumerable<ContactResponseDto>> GetAllContactsAsync();
        Task RemoveContactAsync(Guid contactId);
    }
}
