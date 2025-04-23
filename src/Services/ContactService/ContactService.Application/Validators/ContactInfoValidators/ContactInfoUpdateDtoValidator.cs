using ContactService.Application.DTOs.ContactInformation;
using ContactService.Domain.Enums;
using FluentValidation;

namespace ContactService.Application.Validators.ContactInfoValidators
{
    public class ContactInfoUpdateDtoValidator : AbstractValidator<ContactInfoUpdateDto>
    {
        public ContactInfoUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID boş olamaz.");
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Geçersiz iletişim tipi. iletişim tipi Phone, Email, Location olabilir. ");
            RuleFor(x => x.InfoContent)
                .NotEmpty().WithMessage("İletişim bilgisi boş olamaz.");
            //Phone
            When(x => x.Type == ContactInfoType.Phone, () =>
            {
                RuleFor(x => x.InfoContent)
                .Matches(@"^\+?[0-9\s\-]{10,15}$").WithName("Telefon").WithMessage("Geçerli bir Telefon numarası giriniz.");
            });
            //Email
            When(x => x.Type == ContactInfoType.Email, () =>
            {
                RuleFor(x => x.InfoContent)
                .EmailAddress().WithName("Email").WithMessage("Geçerli bir Email adresi giriniz.");
            });
            //Location
            When(x => x.Type == ContactInfoType.Location, () =>
            {
                RuleFor(x => x.InfoContent)
                .MinimumLength(3).WithName("Lokasyon").WithMessage("Geçerli bir Konum bilgisi giriniz.");
            });
        }
    }
}
