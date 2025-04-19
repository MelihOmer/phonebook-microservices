using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Infrastructure.Persistence
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Company).IsRequired().HasMaxLength(150);
            builder.HasMany(x => x.ContactInformations)
                .WithOne()
                .HasForeignKey(x => x.ContanctId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
