using AutoMapper;
using ContactService.Application.DTOs.Contact;
using ContactService.Application.Interfaces;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.Entities;
using ContactService.Tests.FakeObjects;
using FluentAssertions;
using Moq;
using PhonebookMicroservices.Shared.Exceptions;
using System.Linq.Expressions;

namespace ContactService.Tests.Application.Features.Contacts
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly FakeDbContext _dbContext;
        private readonly ContactService.Infrastructure.Services.ContactService _contactService;
        public ContactServiceTests()
        {
            _dbContext = new FakeDbContext(new());
            _mockRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _contactService = new ContactService.Infrastructure.Services.ContactService(_mockRepository.Object, _dbContext, _mockMapper.Object);
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
        [Fact]
        public async Task GetContactByIdAsync_WhenContactExists_ShouldReturnMapperDto()
        {
            //Arrange
            var id = Guid.NewGuid();
            var contact = new Contact
            {
                Id = id,
                Firstname = "Melih Ömer",
                Lastname = "KAMAR",
                Company = "Company x"
            };
            var expectedDto = new ContactResponseDto(contact.Id, contact.Firstname, contact.Lastname, contact.Company);
            _mockRepository.Setup(repo => repo.GetContactByIdAsync(id))
                .ReturnsAsync(contact);
            _mockMapper.Setup(map => map.Map<ContactResponseDto>(contact))
                .Returns(expectedDto);
            //Act
            var result = await _contactService.GetContactByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ContactResponseDto>();
            result.Id.Should().Be(id);
            result.Firstname.Should().Be("Melih Ömer");
            result.Company.Should().Be("Company x");
        }
        [Fact]
        public async Task GetContactByIdAsync_WhenContactDoesNotExists_ShouldThrowNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetContactByIdAsync(id))
                .ReturnsAsync((Contact)null!);
            //Act
            var act = async () => await _contactService.GetContactByIdAsync(id);

            //Assert
            await act.Should()
                .ThrowAsync<NotFoundException>();
        }
        [Fact]
        public async Task GetContactsByCompanyNameAsync_WhenContactExists_ShouldReturnMappedDtos()
        {
            //Arrange
            var company = "Poseidon BT";
            var contacts = new List<Contact>
            {
                new (){Id = Guid.NewGuid(),Firstname = "Melih Ömer",Lastname = "KAMAR",Company="Poseidon BT"},
                new (){Id = Guid.NewGuid(),Firstname = "Tuğba",Lastname = "KAMAR",Company="Company X"},
            };
            var filteredContacts = contacts.Where(x => x.Company.Equals(company, StringComparison.OrdinalIgnoreCase));
            var expectedDtos = contacts.Where(x => x.Company.Equals(company, StringComparison.OrdinalIgnoreCase)).Select(c =>
            new ContactResponseDto(c.Id, c.Firstname, c.Lastname, c.Company)).ToList();

            _mockRepository.Setup(r =>
            r.GetContactsByExpressionAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(filteredContacts);

            _mockMapper.Setup(m =>
            m.Map<IEnumerable<ContactResponseDto>>(filteredContacts))
                .Returns(expectedDtos);
            //Act
            var result = await _contactService.GetContactsByCompanyNameAsync(company);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().Should().BeOfType<ContactResponseDto>();
            result.First().Company.Should().Be(company);
        }
        [Fact]
        public async Task GetContactsByCompanyNameAsync_WhenNoContactsFound_ShouldThrowNotFoundException()
        {
            //Arrange
            var company = "Poseidon BT";

            _mockRepository.Setup(r =>
            r.GetContactsByExpressionAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(new List<Contact>());

            //Act
            var act = async () => await _contactService.GetContactsByCompanyNameAsync(company);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

        }
        [Fact]
        public async Task CreateContactAsync_WhenValidRequest_ShouldReturnMappedDtos()
        {
            //Arrange
            var createDto = new ContactCreateDto("Melih Ömer", "KAMAR", "Poseidon BT");
            var createdEntity = new Contact()
            {
                Id = Guid.NewGuid(),
                Firstname = "Melih Ömer",
                Lastname = "KAMAR",
                Company = "Poseidon BT"
            };
            var expectedDto = new ContactResponseDto
                (createdEntity.Id, createdEntity.Firstname, createdEntity.Lastname, createdEntity.Company);
            _mockMapper.Setup(m => m.Map<Contact>(createDto)).Returns(createdEntity);
            _mockRepository.Setup(r => r.CreateContactAsync(createdEntity))
                .ReturnsAsync(createdEntity);
            _mockMapper.Setup(m => m.Map<ContactResponseDto>(createdEntity))
                .Returns(expectedDto);

            //Act
            var result = await _contactService.CreateContactAsync(createDto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ContactResponseDto>();
            result.Firstname.Should().Be("Melih Ömer");
        }
        [Fact]
        public async Task UpdateContactAsync_WhenValidRequest_ShouldReturnMappedDtos()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();

            var updateDto = new ContactUpdateDto(contactId, "Melih Ömer Updated", "KAMAR Updated", "Company Updated");

            var existingContact = new Contact
            {
                Id = contactId,
                Firstname = "Melih Ömer",
                Lastname = "KAMAR",
                Company = "Company"
            };

            var mappedEntity = new Contact
            {
                Id = contactId,
                Firstname = updateDto.Firstname,
                Lastname = updateDto.Lastname,
                Company = updateDto.Company
            };

            var updatedResponse = new ContactResponseDto(contactId, updateDto.Firstname, updateDto.Lastname, updateDto.Company);

            _mockRepository.Setup(r => r.GetContactByIdAsync(contactId)).ReturnsAsync(existingContact);
            _mockMapper.Setup(m => m.Map<Contact>(updateDto)).Returns(mappedEntity);
            _mockRepository.Setup(r => r.UpdateContactAsync(mappedEntity)).ReturnsAsync(mappedEntity);
            _mockMapper.Setup(m => m.Map<ContactResponseDto>(mappedEntity)).Returns(updatedResponse);


            // Act
            var result = await _contactService.UpdateContactAsync(updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(contactId);
            result.Firstname.Should().Be("Melih Ömer Updated");

            _mockRepository.Verify(r => r.GetContactByIdAsync(contactId), Times.Once);
            _mockRepository.Verify(r => r.UpdateContactAsync(mappedEntity), Times.Once);
            _mockMapper.Verify(m => m.Map<Contact>(updateDto), Times.Once);
            _mockMapper.Verify(m => m.Map<ContactResponseDto>(mappedEntity), Times.Once);
        }
        [Fact]
        public async Task UpdateContactAsync_WhenContactNotExists_ShouldThrowNotFoundException()
        {
            //Arrange
            var contactId = Guid.NewGuid();
            var updateDto = new ContactUpdateDto(contactId, "Melih Ömer Updated", "KAMAR Updated", "Company Updated");
            _mockRepository.Setup(r => r.GetContactByIdAsync(contactId))
                .ReturnsAsync((Contact)null!);
            //Act
            var act = async () => await _contactService.UpdateContactAsync(updateDto);
            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
            _mockRepository.Verify(r => r.GetContactByIdAsync(contactId), Times.Once);
            _mockRepository.Verify(r => r.UpdateContactAsync(It.IsAny<Contact>()), Times.Never);

        }
        [Fact]
        public async Task RemoveContactAsync_WhenContactExists_ShouldRemoveContactAndSave()
        {
            //Arrange
            Guid contactId = Guid.NewGuid();
            var existingContact = new Contact() { Id = contactId, Firstname = "Melih Ömer", Lastname = "KAMAR", Company = "Poseidon BT" };
            _mockRepository.Setup(r => r.GetContactByIdAsync(contactId))
                .ReturnsAsync(existingContact);
            _mockRepository.Setup(r => r.RemoveContactAsync(contactId))
                .Returns(Task.CompletedTask);

            //Act
            await _contactService.RemoveContactAsync(contactId);

            //Assert
            _mockRepository.Verify(r => r.GetContactByIdAsync(contactId), Times.Once);
            _mockRepository.Verify(r => r.RemoveContactAsync(contactId), Times.Once);
        }
        [Fact]
        public async Task RemoveContactAsync_WhenContactDoesNotExists_ShouldThrowNotFoundException()
        {
            //Arrange
            Guid contactId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetContactByIdAsync(contactId))
                .ReturnsAsync((Contact)null!);
            //Act
            var act = async () => await _contactService.RemoveContactAsync(contactId);
            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
            _mockRepository.Verify(r => r.GetContactByIdAsync(contactId), Times.Once);
            _mockRepository.Verify(r => r.RemoveContactAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
