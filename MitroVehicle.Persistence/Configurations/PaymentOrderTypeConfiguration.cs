using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Persistence.Configurations
{
    public class PaymentOrderTypeConfiguration : BaseEntityTypeConfiguration<PaymentOrder>
    {
        public void Configure(EntityTypeBuilder<PaymentOrder> builder)
        {
            builder.ToTable(nameof(PaymentOrder));

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.DataPayment)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(p => p.Location)
                .WithMany(l => l.PaymentOrders)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}




