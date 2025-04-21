using AutoMapper;
using ContactService.Application.DTOs.Contact;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace ContactService.Tests.Application.Features.Contacts
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ContactService.Infrastructure.Services.ContactService _contactService;
        public ContactServiceTests()
        {
            _mockRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _contactService = new ContactService.Infrastructure.Services.ContactService(_mockRepository.Object, null, _mockMapper.Object);
        }
        [Fact]
        public async Task GetAllContactsAsync_WhenContactExist_ShouldReturnMappedDtos()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new() { Id = Guid.NewGuid(), Firstname = "Melih Ömer", Lastname = "KAMAR", Company = "Company x" },
                new() { Id = Guid.NewGuid(), Firstname = "Tuğba", Lastname = "KAMAR", Company = "Company y" },
                new() { Id = Guid.NewGuid(), Firstname = "Ali", Lastname = "VELİ", Company = "Company z" }
            };

            var contactDtos = contacts.Select(c =>
                new ContactResponseDto(c.Id, c.Firstname, c.Lastname, c.Company)).ToList();
           

            _mockRepository.Setup(repo => repo.GetAllContactsAsync())
                           .ReturnsAsync(contacts);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ContactResponseDto>>(contacts))
                       .Returns(contactDtos);

            // Act
            var result = await _contactService.GetAllContactsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.First().Firstname.Should().Be("Melih Ömer");
            result.First().GetType().Should().Be(typeof(ContactResponseDto));

            _mockRepository.Verify(r => r.GetAllContactsAsync(), Times.Once());
            _mockMapper.Verify(m => m.Map<IEnumerable<ContactResponseDto>>(contacts), Times.Once());
        }
    }
}
