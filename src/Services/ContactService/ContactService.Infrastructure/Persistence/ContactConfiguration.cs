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
        }
    }
}
