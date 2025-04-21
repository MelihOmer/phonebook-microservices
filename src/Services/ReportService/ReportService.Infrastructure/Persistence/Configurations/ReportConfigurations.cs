using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Persistence.Configurations
{
    public class ReportConfigurations : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("reports");
            builder.Property(x => x.Id)
                .HasColumnName("id");
            builder.Property(x => x.ReportStatus)
                .HasColumnName("report_status");
            builder.Property(x => x.RequestedAt)
                .HasColumnName("requested_at");

            builder.HasMany(x => x.ReportDetails)
                .WithOne(y => y.Report)
                .HasForeignKey(x => x.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
