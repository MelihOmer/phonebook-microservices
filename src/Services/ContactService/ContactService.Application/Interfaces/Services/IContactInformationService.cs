using ContactService.Application.DTOs.ContactInformation;

namespace ContactService.Application.Interfaces.Services
{
    public interface IContactInformationService
    {
        Task<ContactInfoResponseDto> CreateContactInformationAsync(ContactInfoCreateDto contactInfoCreateDto);
        Task<IEnumerable<ContactInfoResponseDto>> GetAllContactInformationsAsync();
        Task<ContactInfoResponseDto> GetContactInformationByIdAsync(Guid id);
        Task<IEnumerable<ContactInfoResponseDto>> GetContactInformationsByContactIdAsync(Guid contactId);
        Task RemoveContactInformationAsync(Guid id);
    }
}
