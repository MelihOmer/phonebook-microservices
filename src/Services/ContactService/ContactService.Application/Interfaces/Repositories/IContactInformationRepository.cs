using ContactService.Domain.Entities;
using System.Linq.Expressions;

namespace ContactService.Application.Interfaces.Repositories
{
    public interface IContactInformationRepository
    {
        Task<IEnumerable<ContactInformation>> GetAllContactInformationAsync();
        Task<ContactInformation> GetContactInformationByIdAsync(Guid id);
        Task<IEnumerable<ContactInformation>> GetContactInformationByExpressionAsync(Expression<Func<ContactInformation,bool>> expression);
        Task<ContactInformation> CreateContactInformationAsync(ContactInformation contactInformation);
        Task<ContactInformation> UpdateContactInformationAsync(ContactInformation contactInformation);
        Task RemoveContactInformationAsync(Guid id);
    }
}
