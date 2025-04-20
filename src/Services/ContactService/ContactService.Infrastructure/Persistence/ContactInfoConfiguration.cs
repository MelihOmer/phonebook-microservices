using ContactService.Domain.Entities;
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

        }
    }
}
