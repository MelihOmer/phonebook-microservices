using AutoMapper;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Application.Interfaces.Services;
using ContactService.Domain.DTOs.Contact;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;

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

        public async Task<ContactResponseDto> CreateContact(ContactCreateDto contactCreateDto)
        {
            var contactCreate = _mapper.Map<Contact>(contactCreateDto);
            var contact =   await _repository.CreateContact(contactCreate);
            await _dbContext.SaveChangesAsync();
            var response = _mapper.Map<ContactResponseDto>(contact);
            return response;
        }

        public async Task RemoveContact(Guid contactId)
        {
             await _repository.RemoveContact(contactId);
            await _dbContext.SaveChangesAsync();
        }
    }
}
