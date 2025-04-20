using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Infrastructure.Repository;
using System.Linq.Expressions;

namespace ContactService.Infrastructure.Repositories
{
    public class ContactRepository : EfGenericRepository<Contact>, IContactRepository
    {
        private readonly AppDbContext _appDbContext;
        public ContactRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            var addedContact = await CreateAsync(contact);
            return addedContact;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task<Contact> GetContactByIdAsync(Guid id)
        {
            var result = await GetByIdAsync(id);
            return result;
        }

        public IQueryable<Contact> GetContactQuery()
        {
            var result = GetQueryable();
            return result;
        }

        public async Task<IEnumerable<Contact>> GetContactsByExpressionAsync(Expression<Func<Contact, bool>> expression)
        {
            var result = await GetAllAsyncByFilter(expression);
            return result;
        }
        public async Task RemoveContactAsync(Guid contactId)
        {
            await DeleteAsync(contactId);
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            var oldEntity = await GetByIdAsync(contact.Id);
            oldEntity.Firstname = contact.Firstname;
            oldEntity.Lastname = contact.Lastname;
            oldEntity.Company = contact.Company;
            var updatedEntity = await UpdateAsync(oldEntity);
            return updatedEntity;
        }
    }
}
