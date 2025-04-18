
using ContactService.Domain.DTOs.Contact;
using ContactService.Domain.Entities;

namespace ContactService.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> CreateContact(Contact contact); 
        Task RemoveContact(Guid contactId);
    }
}
