using ContactService.Application.DTOs.Contact;
using ContactService.Application.Validators.ContactValidators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace ContactService.Tests.Application.Feature.Contact.Validators
{
    public class ContactCreateDtoValidatorTests
    {
        private readonly ContactCreateDtoValidator _validator;
        public ContactCreateDtoValidatorTests()
        {
            _validator = new ContactCreateDtoValidator();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Firstname_Should_Have_Error_When_Empty(string invalidValue)
        {
            var dto = new ContactCreateDto(invalidValue, "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
        [Fact]
        public void Firstname_Should_Have_Error_When_Exceeds_Max_Length()
        {
            var longName = new string('A', 51);
            var dto = new ContactCreateDto(longName, "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
        [Fact]
        public void Firstname_Should_Not_Have_Error_When_Exactly_50_Characters()
        {
            var validName = new string('a', 50);
            var dto = new ContactCreateDto(validName, "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Lastname_Should_Have_Error_When_Empty(string invalidValue)
        {
            var dto = new ContactCreateDto("Melih Ömer", invalidValue, "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Lastname);
        }
        [Fact]
        public void Lastname_Should_Have_Error_When_Exceeds_Max_Length()
        {
            var longName = new string('A', 51);
            var dto = new ContactCreateDto("Melih Ömer", longName, "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Lastname);
        }
        [Fact]
        public void Lastname_Should_Not_Have_Error_When_Exactly_50_Characters()
        {
            var validName = new string('a', 50);
            var dto = new ContactCreateDto("Melih Ömer", validName, "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Company_Should_Have_Error_When_Empty(string invalidValue)
        {
            var dto = new ContactCreateDto("Melih Ömer", "KAMAR", invalidValue);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Company);
        }
        [Fact]
        public void Company_Should_Have_Error_When_Exceeds_Max_Length()
        {
            var longCompanyName = new string('A', 151);
            var dto = new ContactCreateDto("Melih Ömer", "KAMAR", longCompanyName);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Company);
        }
        [Fact]
        public void Company_Should_Not_Have_Error_When_Exactly_150_Characters()
        {
            var validCompanyName = new string('a', 150);
            var dto = new ContactCreateDto("Melih Ömer", "KAMAR", validCompanyName);
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void All_Fields_Valid_Should_Not_Have_Errors()
        {
            var dto = new ContactCreateDto("Melih Ömer", "KAMAR", "Poseidon BT");
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
