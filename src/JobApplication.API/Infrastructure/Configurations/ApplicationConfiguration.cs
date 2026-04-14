using JobApplication.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.API.Infrastructure.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.Property(a => a.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Position)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.JobUrl)
                .HasMaxLength(500);

            builder.Property(a => a.Notes)
                .HasMaxLength(1000);
        }
    }
}
