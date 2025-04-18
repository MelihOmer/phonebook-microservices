using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.DTOs.Contact;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Infrastructure.Repository;

namespace ContactService.Infrastructure.Repositories
{
    public class ContactRepository : EfGenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            var addedContact = await CreateAsync(contact);
            return addedContact;
        }

        public async Task RemoveContact(Guid contactId)
        {
            await DeleteAsync(contactId);
        }
    }
}
