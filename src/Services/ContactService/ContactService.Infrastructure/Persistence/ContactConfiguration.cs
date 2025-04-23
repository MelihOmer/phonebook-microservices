using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Infrastructure.Persistence
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.ToTable("contacts");

            builder.Property(x => x.Firstname).HasColumnName("first_name").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Lastname).HasColumnName("last_name").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Company).HasColumnName("company").IsRequired().HasMaxLength(150);
            builder.HasMany(x => x.ContactInformations)
                .WithOne()
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new Contact()
            {
                Id = Guid.Parse("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"),
                Firstname = "Melih Ömer",
                Lastname = "KAMAR",
                Company = "Poseidon BT",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contact()
            {
                Id = Guid.Parse("ed584624-1260-493e-a206-3bb8b28f82a6"),
                Firstname = "Ali",
                Lastname = "Veli",
                Company = "Company X",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contact()
            {
                Id = Guid.Parse("321e8895-58c2-43ba-970b-0328d7177133"),
                Firstname = "Deleted",
                Lastname = "Contact",
                Company = "Company deleted",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = true
            });
        }
    }
}
