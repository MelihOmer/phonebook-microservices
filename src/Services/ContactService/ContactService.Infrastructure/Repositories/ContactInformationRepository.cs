using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Infrastructure.Repository;
using System.Linq.Expressions;

namespace ContactService.Infrastructure.Repositories
{
    public class ContactInformationRepository : EfGenericRepository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<ContactInformation> CreateContactInformationAsync(ContactInformation contactInformation)
        {
            var result = await CreateAsync(contactInformation);
            return result;
        }

        public async Task<IEnumerable<ContactInformation>> GetAllContactInformationAsync()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task<IEnumerable<ContactInformation>> GetContactInformationByExpressionAsync(Expression<Func<ContactInformation, bool>> expression)
        {
            var result = await GetAllAsyncByFilter(expression);
            return result;
        }

        public async Task<ContactInformation> GetContactInformationByIdAsync(Guid id)
        {
            var result = await GetByIdAsync(id);
            return result;
        }

        public async Task RemoveContactInformationAsync(Guid id)
        {
            await DeleteAsync(id);
        }

        public async Task<ContactInformation> UpdateContactInformationAsync(ContactInformation contactInformation)
        {
            var entity = await GetByIdAsync(contactInformation.Id);
            entity.InfoContent = contactInformation.InfoContent;
            entity.Type = contactInformation.Type;
            var updatedEntity = await UpdateAsync(entity);
            return updatedEntity;
        }
    }
}
