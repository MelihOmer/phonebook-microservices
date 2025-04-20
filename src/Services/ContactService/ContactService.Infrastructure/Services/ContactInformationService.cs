using AutoMapper;
using ContactService.Application.DTOs.ContactInformation;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Application.Interfaces.Services;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using PhonebookMicroservices.Shared.Exceptions;

namespace ContactService.Infrastructure.Services
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly IContactInformationRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public ContactInformationService(AppDbContext dbContext, IMapper mapper, IContactInformationRepository repository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ContactInfoResponseDto> CreateContactInformationAsync(ContactInfoCreateDto contactInfoCreateDto)
        {
            var contactInfo = _mapper.Map<ContactInformation>(contactInfoCreateDto);
            var result = await _repository.CreateContactInformationAsync(contactInfo);
            await _dbContext.SaveChangesAsync();
            var mappingResponse = _mapper.Map<ContactInfoResponseDto>(result);
            return mappingResponse;
        }

        public async Task<ContactInfoResponseDto> GetContactInformationByIdAsync(Guid id)
        {
            var contactInfo = await _repository.GetContactInformationByIdAsync(id);
            if (contactInfo == null)
                throw new NotFoundException($"({id}) ID İletişim bilgisi bulunamadı.");
            var mappingResponse = _mapper.Map<ContactInfoResponseDto>(contactInfo);
            return mappingResponse;
        }

        public async Task<IEnumerable<ContactInfoResponseDto>> GetContactInformationsByContactIdAsync(Guid contactId)
        {
            var contactInfos = await _repository.GetContactInformationByExpressionAsync(x => x.ContanctId == contactId);
            if (!contactInfos.Any())
                throw new NotFoundException($"({contactId}) ID Kişiye ait iletşim bilgisi bulunamadı.");
            var mappingResponse = _mapper.Map<IEnumerable<ContactInfoResponseDto>>(contactInfos);
            return mappingResponse;
        }

        public async Task RemoveContactInformationAsync(Guid id)
        {
            var contactInfo = await _repository.GetContactInformationByIdAsync(id);
            if (contactInfo == null)
                throw new NotFoundException($"({id}) ID İletişim bilgisi bulunamadı.");
            await _repository.RemoveContactInformationAsync(id);
            await _dbContext.SaveChangesAsync();
        }
    }
}
