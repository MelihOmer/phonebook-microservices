using ContactService.Application.Interfaces.Services;
using ContactService.Application.DTOs.Contact;
using Microsoft.AspNetCore.Mvc;
using PhonebookMicroservices.Shared.ResponseTypes;
using ContactService.Application.DTOs.Statistic;

namespace ContactService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IStatisticService _statisticService;

        public ContactsController(IContactService contactService, IStatisticService statisticService)
        {
            _contactService = contactService;
            _statisticService = statisticService;
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
            var response = ApiResponse<ContactWithInformationsResponseDto>.Ok(result, "İşlem Başarılı.");
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
        public async Task<IActionResult> GetContactByCompanyName([FromQuery] string companyName)
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
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] ContactUpdateDto contactUpdateDto)
        {
            var result = await _contactService.UpdateContactAsync(contactUpdateDto);
            var response = ApiResponse<ContactResponseDto>.Ok(result);
            return Ok(response);
        }
        [HttpDelete("{contactId:Guid}")]
        public async Task<IActionResult> RemoveContact(Guid contactId)
        {
            await _contactService.RemoveContactAsync(contactId);
            var response = ApiResponse<object>.Ok(null, $"{contactId} ID Silme işlemi başarılı.");
            return Ok(response);
        }
    }
}
