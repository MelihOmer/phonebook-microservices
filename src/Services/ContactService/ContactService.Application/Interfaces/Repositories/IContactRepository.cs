using ContactService.Domain.Entities;
using System.Linq.Expressions;

namespace ContactService.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> GetContactById(Guid id);
        IQueryable<Contact> GetContactQuery();
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<IEnumerable<Contact>> GetContactsByExpressionAsync(Expression<Func<Contact,bool>> expression);
        Task<Contact> CreateContact(Contact contact);
        Task RemoveContact(Guid contactId);
    }
}
