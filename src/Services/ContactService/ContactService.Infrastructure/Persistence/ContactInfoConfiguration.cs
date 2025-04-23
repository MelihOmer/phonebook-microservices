using ContactService.Domain.Entities;
using ContactService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Infrastructure.Persistence
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.ToTable("contact_informations");
            builder.Property(x => x.ContactId).HasColumnName("contact_id").IsRequired();
            builder.Property(x => x.Type).HasColumnName("type").IsRequired();
            builder.Property(x => x.InfoContent).HasColumnName("content").IsRequired();
            builder.HasOne(x => x.Contact)
                .WithMany(y => y.ContactInformations)
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            #region seed data
            ContactInformation ci1 = new()
            {
                Id = Guid.Parse("2102795b-41dd-4d70-bb91-a321dac58656"),
                ContactId = Guid.Parse("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"),
                Type = ContactInfoType.Phone,
                InfoContent = "0505 090 07 04",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            ContactInformation ci2 = new()
            {
                Id = Guid.Parse("46e06077-cd79-43f1-946b-9970b0e04557"),
                ContactId = Guid.Parse("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"),
                Type = ContactInfoType.Location,
                InfoContent = "İstanbul",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            ContactInformation ci3 = new()
            {
                Id = Guid.Parse("1ae461cd-0c34-4999-aa6d-5d8a8553454e"),
                ContactId = Guid.Parse("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"),
                Type = ContactInfoType.Location,
                InfoContent = "Mersin",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            ContactInformation ci4 = new()
            {
                Id = Guid.Parse("efaba73d-fb91-4120-9863-dc693b1e61d1"),
                ContactId = Guid.Parse("ed584624-1260-493e-a206-3bb8b28f82a6"),
                Type = ContactInfoType.Phone,
                InfoContent = "0505 505 05 05",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            ContactInformation ci5 = new()
            {
                Id = Guid.Parse("2b97ff98-39c3-4cc3-a2ee-8fc5a596b818"),
                ContactId = Guid.Parse("ed584624-1260-493e-a206-3bb8b28f82a6"),
                Type = ContactInfoType.Location,
                InfoContent = "İstanbul",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            #endregion
            builder.HasData(ci1, ci2, ci3, ci4, ci5);

        }
    }
}
