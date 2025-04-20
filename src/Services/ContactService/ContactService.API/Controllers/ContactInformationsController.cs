using ContactService.Application.DTOs.ContactInformation;
using ContactService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using PhonebookMicroservices.Shared.ResponseTypes;

namespace ContactService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactInformationsController : ControllerBase
    {
        private readonly IContactInformationService _contactInformationService;

        public ContactInformationsController(IContactInformationService contactInformationService)
        {
            _contactInformationService = contactInformationService;
        }
        [HttpGet("contact-id")]
        public async Task<IActionResult> GetAllContactInformationsByContactId([FromQuery]Guid contactId)
        {
            var result = await _contactInformationService.GetContactInformationsByContactIdAsync(contactId);
            var response = ApiResponse<IEnumerable<ContactInfoResponseDto>>.Ok(result,"İşlem Başarılı.");
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContactInformation(ContactInfoCreateDto contactInfoCreateDto)
        {
            var result = await _contactInformationService.CreateContactInformationAsync(contactInfoCreateDto);
            var response = ApiResponse<ContactInfoResponseDto>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
    }
}
