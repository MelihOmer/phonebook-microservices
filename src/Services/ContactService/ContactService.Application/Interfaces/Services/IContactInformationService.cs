using ContactService.Application.DTOs.ContactInformation;

namespace ContactService.Application.Interfaces.Services
{
    public interface IContactInformationService
    {
        Task<IEnumerable<ContactInfoResponseDto>> GetAllContactInformationsAsync();
        Task<ContactInfoResponseDto> GetContactInformationByIdAsync(Guid id);
        Task<IEnumerable<ContactInfoResponseDto>> GetContactInformationsByContactIdAsync(Guid contactId);
        Task<ContactInfoResponseDto> CreateContactInformationAsync(ContactInfoCreateDto contactInfoCreateDto);
        Task<ContactInfoResponseDto> UpdateContactInformationAsync(ContactInfoUpdateDto updateDto);
        Task RemoveContactInformationAsync(Guid id);
    }
}
