using BusinessInfo.Domain.Entities;
using BusinessInfo.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MitroVehicle.Persistence.Configurations
{
    public class VehicleTypeConfiguration : BaseEntityTypeConfiguration<Vehicle>
    {
        public override void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable(nameof(Vehicle));
            builder.Property(x => x.NameVehicle)
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            builder.Property(x => x.Plate)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.Model)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.Renavam)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.Brand)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.Collor)
                           .HasColumnType("varchar(255)")
                           .IsRequired();

            builder.Property(x => x.Year)
                           .HasColumnType("int")
                           .IsRequired();

            builder.Property(x => x.DailyRate)
                           .HasColumnType("decimal(18,2)")
                           .IsRequired();

            builder.Property(x => x.TypeVechicle)
                            .HasColumnType("int")
                            .IsRequired();

            builder.HasOne(v => v.Issuer)
              .WithMany(i => i.Vehicles)
              .HasForeignKey(v => v.IssuerId)
              .OnDelete(DeleteBehavior.Restrict);
        }


    }
}