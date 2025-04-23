using AutoMapper;
using ContactService.Application.DTOs.ContactInformation;
using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.Entities;
using ContactService.Domain.Enums;
using ContactService.Infrastructure.Services;
using ContactService.Tests.FakeObjects;
using FluentAssertions;
using Moq;
using PhonebookMicroservices.Shared.Exceptions;
using System.Linq.Expressions;

namespace ContactService.Tests.Infrastructure.Services.ContactInformations
{
    public class ContactInformationServiceTests
    {
        private readonly Mock<IContactInformationRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly FakeDbContext _fakeDbContext;
        private readonly ContactInformationService _service;
        public ContactInformationServiceTests()
        {
            _mockRepository = new Mock<IContactInformationRepository>();
            _mockMapper = new Mock<IMapper>();
            _fakeDbContext = new FakeDbContext(new());

            _service = new ContactInformationService(_fakeDbContext, _mockMapper.Object, _mockRepository.Object);
        }
        [Fact]
        public async Task GetAllContactInformationsAsync_ShouldReturnMappedDtos()
        {
            // Arrange
            var entities = new List<ContactInformation>
            {
                new() { Id = Guid.NewGuid(), ContactId = Guid.NewGuid(), Type = ContactInfoType.Email, InfoContent = "test@maill.com" },
                new() { Id = Guid.NewGuid(), ContactId = Guid.NewGuid(), Type = ContactInfoType.Phone, InfoContent = "0505 090 07 04" }
            };

            var dtos = entities.Select(e =>
                new ContactInfoResponseDto(e.Id, e.ContactId, e.Type, e.InfoContent)).ToList();

            _mockRepository.Setup(r => r.GetAllContactInformationAsync())
                .ReturnsAsync(entities);

            _mockMapper.Setup(m => m.Map<IEnumerable<ContactInfoResponseDto>>(entities))
                .Returns(dtos);

            // Act
            var result = await _service.GetAllContactInformationsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().InfoContent.Should().Be("test@maill.com");
            result.First().Should().BeOfType<ContactInfoResponseDto>();

            _mockRepository.Verify(r => r.GetAllContactInformationAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<ContactInfoResponseDto>>(entities), Times.Once);
        }
        [Fact]
        public async Task GetContactInformationByIdAsync_WhenExists_ShouldReturnMappedDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new ContactInformation
            {
                Id = id,
                ContactId = Guid.NewGuid(),
                Type = ContactInfoType.Email,
                InfoContent = "melihomerkamar@mail.com"
            };

            var dto = new ContactInfoResponseDto(entity.Id, entity.ContactId, entity.Type, entity.InfoContent);

            _mockRepository.Setup(r => r.GetContactInformationByIdAsync(id)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<ContactInfoResponseDto>(entity)).Returns(dto);

            // Act
            var result = await _service.GetContactInformationByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.InfoContent.Should().Be("melihomerkamar@mail.com");

            _mockRepository.Verify(r => r.GetContactInformationByIdAsync(id), Times.Once);
            _mockMapper.Verify(m => m.Map<ContactInfoResponseDto>(entity), Times.Once);
        }
        [Fact]
        public async Task GetContactInformationByIdAsync_WhenNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetContactInformationByIdAsync(id)).ReturnsAsync((ContactInformation)null!);

            // Act
            Func<Task> act = async () => await _service.GetContactInformationByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"({id}) ID İletişim bilgisi bulunamadı.");

            _mockRepository.Verify(r => r.GetContactInformationByIdAsync(id), Times.Once);
            _mockMapper.Verify(m => m.Map<ContactInfoResponseDto>(It.IsAny<ContactInformation>()), Times.Never);
        }
        [Fact]
        public async Task GetContactInformationsByContactIdAsync_WhenDataExists_ShouldReturnMappedDtos()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var entities = new List<ContactInformation>
    {
        new() { Id = Guid.NewGuid(), ContactId = contactId, Type = ContactInfoType.Email, InfoContent = "melih@mail.com" },
        new() { Id = Guid.NewGuid(), ContactId = contactId, Type = ContactInfoType.Phone, InfoContent = "0505 090 07 04" }
    };

            var dtos = entities.Select(e =>
                new ContactInfoResponseDto(e.Id, e.ContactId, e.Type, e.InfoContent)).ToList();

            _mockRepository.Setup(r =>
                r.GetContactInformationByExpressionAsync(It.IsAny<Expression<Func<ContactInformation, bool>>>()))
                .ReturnsAsync(entities);

            _mockMapper.Setup(m => m.Map<IEnumerable<ContactInfoResponseDto>>(entities)).Returns(dtos);

            // Act
            var result = await _service.GetContactInformationsByContactIdAsync(contactId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeOfType<List<ContactInfoResponseDto>>();
            result.First().ContactId.Should().Be(contactId);

            _mockRepository.Verify(r => r.GetContactInformationByExpressionAsync(It.IsAny<Expression<Func<ContactInformation, bool>>>()), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<ContactInfoResponseDto>>(entities), Times.Once);
        }
        [Fact]
        public async Task GetContactInformationsByContactIdAsync_WhenDataDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var contactId = Guid.NewGuid();

            _mockRepository.Setup(r =>
                r.GetContactInformationByExpressionAsync(It.IsAny<Expression<Func<ContactInformation, bool>>>()))
                .ReturnsAsync(new List<ContactInformation>());

            // Act
            Func<Task> act = async () => await _service.GetContactInformationsByContactIdAsync(contactId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"({contactId}) ID Kişiye ait iletşim bilgisi bulunamadı.");

            _mockMapper.Verify(m => m.Map<IEnumerable<ContactInfoResponseDto>>(It.IsAny<IEnumerable<ContactInformation>>()), Times.Never);
        }
        [Fact]
        public async Task CreateContactInformationAsync_WhenValidInput_ShouldReturnMappedDto()
        {
            // Arrange
            var createDto = new ContactInfoCreateDto(
                Guid.NewGuid(),
                ContactInfoType.Phone,
                "0505 090 07 04"
            );

            var entityToCreate = new ContactInformation
            {
                Id = Guid.NewGuid(),
                ContactId = createDto.ContactId,
                Type = createDto.Type,
                InfoContent = createDto.InfoContent
            };

            var createdEntity = entityToCreate;

            var responseDto = new ContactInfoResponseDto(
                createdEntity.Id,
                createdEntity.ContactId,
                createdEntity.Type,
                createdEntity.InfoContent
            );

            _mockMapper.Setup(m => m.Map<ContactInformation>(createDto)).Returns(entityToCreate);
            _mockRepository.Setup(r => r.CreateContactInformationAsync(entityToCreate)).ReturnsAsync(createdEntity);
            _mockMapper.Setup(m => m.Map<ContactInfoResponseDto>(createdEntity)).Returns(responseDto);

            // Act
            var result = await _service.CreateContactInformationAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(createdEntity.Id);
            result.Should().BeOfType<ContactInfoResponseDto>();
            result.InfoContent.Should().Be("0505 090 07 04");
            _mockMapper.Verify(m => m.Map<ContactInformation>(createDto), Times.Once);
            _mockRepository.Verify(r => r.CreateContactInformationAsync(entityToCreate), Times.Once);
            _mockMapper.Verify(m => m.Map<ContactInfoResponseDto>(createdEntity), Times.Once);
        }
        [Fact]
        public async Task UpdateContactInformationAsync_WhenEntityExists_ShouldReturnUpdatedDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new ContactInfoUpdateDto(id, ContactInfoType.Email, "melihomerkamar0@gmail.com");

            var existingEntity = new ContactInformation
            {
                Id = id,
                ContactId = Guid.NewGuid(),
                Type = ContactInfoType.Email,
                InfoContent = "mok0@email.com"
            };

            var updatedEntity = new ContactInformation
            {
                Id = id,
                ContactId = existingEntity.ContactId,
                Type = updateDto.Type,
                InfoContent = updateDto.InfoContent
            };

            var expectedDto = new ContactInfoResponseDto(id, updatedEntity.ContactId, updateDto.Type, updateDto.InfoContent);

            _mockRepository.Setup(r => r.GetContactInformationByIdAsync(id)).ReturnsAsync(existingEntity);
            _mockMapper.Setup(m => m.Map<ContactInformation>(updateDto)).Returns(updatedEntity);
            _mockRepository.Setup(r => r.UpdateContactInformationAsync(updatedEntity)).ReturnsAsync(updatedEntity);
            _mockMapper.Setup(m => m.Map<ContactInfoResponseDto>(updatedEntity)).Returns(expectedDto);

            // Act
            var result = await _service.UpdateContactInformationAsync(updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.InfoContent.Should().Be("melihomerkamar0@gmail.com");

            _mockRepository.Verify(r => r.GetContactInformationByIdAsync(id), Times.Once);
            _mockRepository.Verify(r => r.UpdateContactInformationAsync(updatedEntity), Times.Once);
            _mockMapper.Verify(m => m.Map<ContactInformation>(updateDto), Times.Once);
            _mockMapper.Verify(m => m.Map<ContactInfoResponseDto>(updatedEntity), Times.Once);
        }
        [Fact]
        public async Task UpdateContactInformationAsync_WhenEntityNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new ContactInfoUpdateDto(id, ContactInfoType.Phone, "111111");

            _mockRepository.Setup(r => r.GetContactInformationByIdAsync(id)).ReturnsAsync((ContactInformation)null!);

            // Act
            Func<Task> act = async () => await _service.UpdateContactInformationAsync(updateDto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"({id}) ID İletişim bilgisi bulunamadı.");

            _mockRepository.Verify(r => r.UpdateContactInformationAsync(It.IsAny<ContactInformation>()), Times.Never);
            _mockMapper.Verify(m => m.Map<ContactInformation>(updateDto), Times.Never);
            _mockMapper.Verify(m => m.Map<ContactInfoResponseDto>(It.IsAny<ContactInformation>()), Times.Never);
        }
        [Fact]
        public async Task RemoveContactInformationAsync_WhenEntityExists_ShouldDeleteSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingEntity = new ContactInformation
            {
                Id = id,
                ContactId = Guid.NewGuid(),
                Type = ContactInfoType.Location,
                InfoContent = "İstanbul"
            };

            _mockRepository.Setup(r => r.GetContactInformationByIdAsync(id)).ReturnsAsync(existingEntity);
            _mockRepository.Setup(r => r.RemoveContactInformationAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _service.RemoveContactInformationAsync(id);

            // Assert
            _mockRepository.Verify(r => r.GetContactInformationByIdAsync(id), Times.Once);
            _mockRepository.Verify(r => r.RemoveContactInformationAsync(id), Times.Once);
        }
        [Fact]
        public async Task RemoveContactInformationAsync_WhenEntityNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRepository.Setup(r => r.GetContactInformationByIdAsync(id)).ReturnsAsync((ContactInformation)null!);

            // Act
            Func<Task> act = async () => await _service.RemoveContactInformationAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"({id}) ID İletişim bilgisi bulunamadı.");

            _mockRepository.Verify(r => r.RemoveContactInformationAsync(It.IsAny<Guid>()), Times.Never);
        }




    }
}
