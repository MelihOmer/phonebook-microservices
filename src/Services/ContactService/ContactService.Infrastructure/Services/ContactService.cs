using AutoMapper;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Application.Interfaces.Services;
using ContactService.Application.DTOs.Contact;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using PhonebookMicroservices.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using ContactService.Application.DTOs.ContactInformation;

namespace ContactService.Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ContactService(IContactRepository repository, AppDbContext dbContext, IMapper mapper)
        {
            _repository = repository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ContactResponseDto> CreateContactAsync(ContactCreateDto contactCreateDto)
        {
            var contactCreate = _mapper.Map<Contact>(contactCreateDto);
            var contact = await _repository.CreateContactAsync(contactCreate);
            await _dbContext.SaveChangesAsync();
            var response = _mapper.Map<ContactResponseDto>(contact);
            return response;
        }

        public async Task<IEnumerable<ContactResponseDto>> GetAllContactsAsync()
        {
            var result = await _repository.GetAllContactsAsync();
            var mappingResult = _mapper.Map<IEnumerable<ContactResponseDto>>(result);
            return mappingResult;
        }

        public async Task<ContactResponseDto> GetContactByIdAsync(Guid id)
        {
            var result = await _repository.GetContactByIdAsync(id);
            if (result is null)
                throw new NotFoundException($"({id}) ID Kişi Bulunamadı.");
            var mappingResult = _mapper.Map<ContactResponseDto>(result);
            return mappingResult;
        }

        public async Task<IEnumerable<ContactResponseDto>> GetContactsByCompanyNameAsync(string companyName)
        {
            var result = await _repository.GetContactsByExpressionAsync(x => x.Company.Equals(companyName));
            if (!result.Any())
                throw new NotFoundException($"({companyName}) Firma İsimli, Kişi Kayıdı Bulunamadı.");
            var mappingResult = _mapper.Map<IEnumerable<ContactResponseDto>>(result);
            return mappingResult;
        }

        public async Task<ContactWithInformationsResponseDto> GetContactWithInformationsAsync(Guid contactId)
        {

            var result = await _repository.GetContactQuery()
                .Where(x => x.Id == contactId)
                .AsNoTracking()
                .AsSplitQuery()
                .Select(x => new ContactWithInformationsResponseDto()
                {
                    Contact = new ContactResponseDto(x.Id, x.Firstname, x.Lastname, x.Company),
                    ContactInformations = x.ContactInformations
                    .Select(ci => new ContactInfoListDto(ci.Id, ci.Type, ci.InfoContent))
                    .ToList()
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task RemoveContactAsync(Guid contactId)
        {
            var contact = await _repository.GetContactByIdAsync(contactId);
            if (contact is null)
                throw new NotFoundException($"({contactId}) ID Silinecek Kişi Bulunamadı.");
            await _repository.RemoveContactAsync(contactId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ContactResponseDto> UpdateContactAsync(ContactUpdateDto contactUpdateDto)
        {
            var contact = await _repository.GetContactByIdAsync(contactUpdateDto.Id);
            if (contact is null)
                throw new NotFoundException($"({contactUpdateDto.Id}) ID Güncellenecek Kişi Bulunamadı.");
            var entity = _mapper.Map<Contact>(contactUpdateDto);
            var updatedContact = await _repository.UpdateContactAsync(entity);
            await _dbContext.SaveChangesAsync();
            var result = _mapper.Map<ContactResponseDto>(updatedContact);
            return result;

        }
    }
}
