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
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetContactInformationById(Guid id)
        {
            var result = await _contactInformationService.GetContactInformationByIdAsync(id);
            var response = ApiResponse<ContactInfoResponseDto>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContactInformations()
        {
            var result = await _contactInformationService.GetAllContactInformationsAsync();
            var response = ApiResponse<IEnumerable<ContactInfoResponseDto>>.Ok(result);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContactInformationAsync([FromBody]ContactInfoUpdateDto contactInfoUpdateDto)
        {
            var result = await _contactInformationService.UpdateContactInformationAsync(contactInfoUpdateDto);
            var response = ApiResponse<ContactInfoResponseDto>.Ok(result,"İşlem Başarılı.");
            return Ok(response);
        }
        [HttpGet("contact-id")]
        public async Task<IActionResult> GetAllContactInformationsByContactId([FromQuery] Guid contactId)
        {
            var result = await _contactInformationService.GetContactInformationsByContactIdAsync(contactId);
            var response = ApiResponse<IEnumerable<ContactInfoResponseDto>>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactInformation([FromBody]ContactInfoCreateDto contactInfoCreateDto)
        {
            var result = await _contactInformationService.CreateContactInformationAsync(contactInfoCreateDto);
            var response = ApiResponse<ContactInfoResponseDto>.Ok(result, "İşlem Başarılı.");
            return Ok(response);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> RemoveContactInformation(Guid id)
        {
            await _contactInformationService.RemoveContactInformationAsync(id);
            var response = ApiResponse<object>.Ok(null, $"({id}) ID Silme işlemi başarılı.");
            return Ok(response);
        }
    }
}
