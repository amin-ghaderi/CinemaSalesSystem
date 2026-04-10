using CinemaSales.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class SalesReportConfiguration : IEntityTypeConfiguration<SalesReport>
{
    public void Configure(EntityTypeBuilder<SalesReport> builder)
    {
        builder.ToTable("SalesReports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReportDate).IsRequired();
        builder.Property(r => r.TotalTicketsSold).IsRequired();

        builder.Property(r => r.TotalRevenue)
            .HasColumnName("TotalRevenue")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.RequiredConverter);

        builder.Property(r => r.TotalVat)
            .HasColumnName("TotalVat")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.RequiredConverter);
    }
}
