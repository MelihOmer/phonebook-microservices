using ContactService.Application.DTOs.Contact;
using ContactService.Application.Validators.ContactValidators;
using FluentValidation.TestHelper;

namespace ContactService.Tests.Application.Feature.Contact.Validators
{
    public class ContactUpdateDtoValidatorTests
    {
        private ContactUpdateValidator _validator;
        public ContactUpdateDtoValidatorTests()
        {
            _validator = new ContactUpdateValidator();
        }
        [Theory]
        [InlineData(null)]
        public void Id_Should_Have_Error_When_Empty(Guid invalidValue)
        {
            var dto = new ContactUpdateDto(invalidValue, "Melih Ömer", "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Firstname_Should_Have_Error_When_Empty(string invalidValue)
        {
            var dto = new ContactUpdateDto(Guid.NewGuid(), invalidValue, "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
        [Fact]
        public void Firstname_Should_Not_Have_Errors()
        {
            var dto = new ContactUpdateDto(Guid.NewGuid(), "Melih Ömer", "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void Firstname_Should_Have_Error_When_Exceeds_Max_Length()
        {
            var longName = new string('A', 51);
            var dto = new ContactUpdateDto(Guid.NewGuid(), longName, "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Lastname_Should_Have_Error_When_Empty(string invalidValue)
        {
            var dto = new ContactUpdateDto(Guid.NewGuid(),  "Melih Ömer",invalidValue,"Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Lastname);
        }
        [Fact]
        public void Lastname_Should_Have_Error_When_Exceeds_Max_Length()
        {
            var longName = new string('A', 51);
            var dto = new ContactUpdateDto(Guid.NewGuid(),"Melih Ömer",longName,"Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Lastname);
        }
        [Fact]
        public void Lastname_Should_Not_Have_Errors()
        {
            var dto = new ContactUpdateDto(Guid.NewGuid(), "Melih Ömer", "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
