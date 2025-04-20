using ContactService.Application.Interfaces.Services;
using ContactService.Application.DTOs.Contact;
using Microsoft.AspNetCore.Mvc;
using PhonebookMicroservices.Shared.ResponseTypes;

namespace ContactService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var result = await _contactService.GetAllContactsAsync();
            var response = ApiResponse<IEnumerable<ContactResponseDto>>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
        [HttpGet("{id}/with-informations")]
        public async Task<IActionResult> GetContactWithInformations(Guid id)
        {
            var result = await _contactService.GetContactWithInformationsAsync(id);
            var response = ApiResponse<ContactWithInformationsResponseDto>.Ok(result);
            return Ok(response);
        }
        [HttpGet("{contactId:Guid}")]
        public async Task<IActionResult> GetContactById(Guid contactId)
        {
            var result = await _contactService.GetContactByIdAsync(contactId);
            var response = ApiResponse<ContactResponseDto>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
        [HttpGet("by-company")]
        public async Task<IActionResult> GetContactByCompanyName([FromQuery]string companyName)
        {
            var result = await _contactService.GetContactsByCompanyNameAsync(companyName);
            var response = ApiResponse<IEnumerable<ContactResponseDto>>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreateDto contactCreateDto)
        {
            var result = await _contactService.CreateContactAsync(contactCreateDto);
            var response = ApiResponse<ContactResponseDto>.Ok(result, "Kayıt işlemi başarılı.");
            return Ok(response);
        }
        [HttpDelete("{contactId:Guid}")]
        public async Task<IActionResult> RemoveContact(Guid contactId)
        {
            await _contactService.RemoveContactAsync(contactId);
            var response = ApiResponse<string>.Ok($"{contactId} ID Silme işlemi başarılı.");
            return Ok(response);
        }
    }
}
