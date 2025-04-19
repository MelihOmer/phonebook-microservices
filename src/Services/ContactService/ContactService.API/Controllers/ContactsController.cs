using ContactService.Application.Interfaces.Services;
using ContactService.Domain.DTOs.Contact;
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
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody]ContactCreateDto contactCreateDto)
        {
            var result = await _contactService.CreateContact(contactCreateDto);
            var response = ApiResponse<ContactResponseDto>.Ok(result,"Kayıt işlemi başarılı.");
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveContact([FromRoute]Guid contactId)
        {
            await _contactService.RemoveContact(contactId);
            var response = ApiResponse<string>.Ok($"{contactId} ID Silme işlemi başarılı.");
            return Ok(response);
        }
    }
}
