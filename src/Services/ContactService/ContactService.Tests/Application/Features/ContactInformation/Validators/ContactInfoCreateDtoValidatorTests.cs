using ContactService.Application.DTOs.ContactInformation;
using ContactService.Application.Validators.ContactInfoValidators;
using ContactService.Domain.Enums;
using FluentValidation.TestHelper;

namespace ContactService.Tests.Application.Features.ContactInformation.Validators
{
    public class ContactInfoCreateDtoValidatorTests
    {
        private readonly ContactInfoCreateDtoValidator _validator=new();

        [Fact]
        public void ContactId_Should_Have_Error_When_Empty()
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.Empty, ContactInfoType.Email, "test@mail.com");
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ContactId);
        }
        [Fact]
        public void Type_Should_Have_Error_When_Invalid_Enum()
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.NewGuid(), (ContactInfoType)10, "test");
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Type);
        }
        [Fact]
        public void InfoContent_Should_Have_Error_When_Empty()
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.NewGuid(),ContactInfoType.Location, "");
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }
        [Fact]
        public void InfoContent_Phone_Should_Have_Error_When_Invalid_Format()
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.NewGuid(), ContactInfoType.Phone, "asd1234asd");
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }
        [Fact]
        public void InfoContent_Email_Should_Have_Error_When_Invalid_Format()
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.NewGuid(), ContactInfoType.Email, "melih.com");
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }
        [Fact]
        public void InfoContent_Location_Should_Have_Error_When_TooShort()
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.NewGuid(), ContactInfoType.Location, "s");
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }
        [Theory]
        [InlineData(ContactInfoType.Phone,"0505 090 07 04")]
        [InlineData(ContactInfoType.Email,"melihomerkamar0@gmail.com")]
        [InlineData(ContactInfoType.Location,"İstanbul")]
        public void All_Valid_Inputs_Should_Not_Have_Errors(ContactInfoType type,string infoContent)
        {
            //Arrange
            var dto = new ContactInfoCreateDto(Guid.NewGuid(), type, infoContent);
            //Act
            var result = _validator.TestValidate(dto);
            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
