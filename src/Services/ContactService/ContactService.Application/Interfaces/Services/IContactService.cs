using ContactService.Domain.DTOs.Contact;

namespace ContactService.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<ContactResponseDto> CreateContact(ContactCreateDto contactCreateDto);
        Task RemoveContact(Guid contactId);
    }
}
