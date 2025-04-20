using ContactService.Application.DTOs.Contact;
using FluentValidation;

namespace ContactService.Application.Validators.ContactValidators
{
    public class ContactCreateDtoValidator : AbstractValidator<ContactCreateDto>
    {
        public ContactCreateDtoValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.")
                .MaximumLength(50).WithMessage("İsim alanı 50 karakterden fazla olamaz.");
            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Soyisim alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyisim alanı 50 karakterden fazla olamaz.");
            RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Şirket adı alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Şirket adı alanı 150 karakterden fazla olamaz.");
        }
    }
}
