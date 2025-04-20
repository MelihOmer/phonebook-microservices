using ContactService.Application.DTOs.ContactInformation;
using ContactService.Domain.Enums;
using FluentValidation;

namespace ContactService.Application.Validators
{
    public class ContactInfoCreateDtoValidator : AbstractValidator<ContactInfoCreateDto>
    {
        public ContactInfoCreateDtoValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty().WithMessage("Kişi ID boş olamaz.");
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Geçersiz iletişim tipi. iletişim tipi Phone, Email, Location olabilir. ");
            RuleFor(x => x.InfoContent)
                .NotEmpty().WithMessage("İletişim bilgisi boş olamaz.");

            //Phone
            When(x => x.Type == ContactInfoType.Phone, () =>
            {
                RuleFor(x => x.InfoContent)
                .Matches(@"^\+?[0-9\s\-]{10,15}$").WithMessage("Geçerli bir Telefon numarası giriniz.");
            });
            //Email
            When(x => x.Type == ContactInfoType.Email, () =>
            {
                RuleFor(x => x.InfoContent)
                .EmailAddress().WithMessage("Geçerli bir Email adresi giriniz.");
            });
            //Location
            When(x => x.Type == ContactInfoType.Location, () =>
            {
                RuleFor(x => x.InfoContent)
                .MinimumLength(3).WithMessage("Geçerli bir konum giriniz.");
            });
        }
    }
}
