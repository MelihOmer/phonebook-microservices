using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.DTOs.Contact;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Infrastructure.Repository;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task<Contact> GetContactById(Guid id)
        {
            var result = await GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<Contact>> GetContactsByExpressionAsync(Expression<Func<Contact, bool>> expression)
        {
            var result = await GetAllAsyncByFilter(expression);
            return result;
        }

        public async Task RemoveContact(Guid contactId)
        {
            await DeleteAsync(contactId);
        }
    }
}
