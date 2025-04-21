using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Persistence.Configurations
{
    public class ReportDetailConfiguration : IEntityTypeConfiguration<ReportDetail>
    {
        public void Configure(EntityTypeBuilder<ReportDetail> builder)
        {
            builder.ToTable("report_details");

            builder.Property(x => x.Id)
                .HasColumnName("id");
            builder.Property(x => x.ReportId)
               .HasColumnName("report_id");
            builder.Property(x => x.PersonCount)
               .HasColumnName("person_count");
            builder.Property(x => x.PhoneCount)
               .HasColumnName("phone_count");
            builder.Property(x => x.Location)
               .HasColumnName("location");

            builder.HasOne(x => x.Report)
                .WithMany(y => y.ReportDetails)
                .HasForeignKey(x => x.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
