using AutoMapper;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Application.Interfaces.Services;
using ContactService.Domain.DTOs.Contact;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using PhonebookMicroservices.Shared.Exceptions;

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
            var contact = await _repository.CreateContact(contactCreate);
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
            var result = await _repository.GetContactById(id);
            if (result is null)
                throw new NotFoundException($"({id}) Kaynak Bulunamadı.");
            var mappingResult = _mapper.Map<ContactResponseDto>(result);
            return mappingResult;
        }

        public async Task<IEnumerable<ContactResponseDto>> GetContactsByCompanyNameAsync(string companyName)
        {
            var result = await _repository.GetContactsByExpressionAsync(x => x.Company.Equals(companyName));
            if (result is null)
                throw new NotFoundException($"({companyName}) Kaynak Bulunamadı.");
            var mappingResult = _mapper.Map<IEnumerable<ContactResponseDto>>(result);
            return mappingResult;
        }

        public async Task RemoveContactAsync(Guid contactId)
        {
            await _repository.RemoveContact(contactId);
            await _dbContext.SaveChangesAsync();
        }
    }
}
