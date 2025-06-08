using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Persistence.Configurations
{
    public class LocationTypeConfiguration : BaseEntityTypeConfiguration<Location>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(nameof(Location));
            builder.Property(x => x.DataStart)
                .IsRequired(false);

            builder.Property(x => x.DataEnd)
                .IsRequired(false);

            builder.Property(x => x.ValueTotal)
                            .HasColumnType("decimal(10,2)")
                            .IsRequired();

            builder.Property(x => x.Status)
                            .IsRequired();

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
