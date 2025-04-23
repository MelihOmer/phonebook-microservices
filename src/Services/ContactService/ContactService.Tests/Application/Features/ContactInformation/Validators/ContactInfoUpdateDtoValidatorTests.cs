using ContactService.Application.DTOs.ContactInformation;
using ContactService.Application.Validators.ContactInfoValidators;
using ContactService.Domain.Enums;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Tests.Application.Features.ContactInformation.Validators
{
    public class ContactInfoUpdateDtoValidatorTests
    {
        private readonly ContactInfoUpdateDtoValidator _validator = new();

        [Fact]
        public void Id_Should_Have_Error_When_Empty()
        {
            var dto = new ContactInfoUpdateDto(Guid.Empty, ContactInfoType.Phone, "0505 090 07 04");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Type_Should_Have_Error_When_Invalid()
        {
            var dto = new ContactInfoUpdateDto(Guid.NewGuid(), (ContactInfoType)999, "test");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Fact]
        public void InfoContent_Should_Have_Error_When_Empty()
        {
            var dto = new ContactInfoUpdateDto(Guid.NewGuid(), ContactInfoType.Email, "");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }

        [Fact]
        public void Phone_Should_Have_Error_When_Invalid_Format()
        {
            var dto = new ContactInfoUpdateDto(Guid.NewGuid(), ContactInfoType.Phone, "ad1213xxxcs");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }

        [Fact]
        public void Email_Should_Have_Error_When_Invalid()
        {
            var dto = new ContactInfoUpdateDto(Guid.NewGuid(), ContactInfoType.Email, "test.comm");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }

        [Fact]
        public void Location_Should_Have_Error_When_TooShort()
        {
            var dto = new ContactInfoUpdateDto(Guid.NewGuid(), ContactInfoType.Location, "A");
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.InfoContent);
        }

        [Theory]
        [InlineData(ContactInfoType.Phone, "0505 090 07 04")]
        [InlineData(ContactInfoType.Email, "melihomerkamar0@gmail.com")]
        [InlineData(ContactInfoType.Location, "İstanbul")]
        public void All_Valid_Inputs_Should_Not_Have_Errors(ContactInfoType type, string content)
        {
            var dto = new ContactInfoUpdateDto(Guid.NewGuid(), type, content);
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
