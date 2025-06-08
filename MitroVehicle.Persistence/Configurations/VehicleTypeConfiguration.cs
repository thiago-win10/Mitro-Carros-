using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Persistence.Configurations
{
    public class VehicleTypeConfiguration : BaseEntityTypeConfiguration<Vehicle>
    {
        public override void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable(nameof(Vehicle));
            builder.Property(x => x.LicensePlate)
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            builder.Property(x => x.ClientId)
                .IsRequired(false);


            builder.Property(x => x.Year)
                .IsRequired();

            builder.Property(x => x.NameVehicle)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.UF)
                            .HasColumnType("varchar(2)")
                            .IsRequired();

            builder.Property(x => x.Renavam)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.Color)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.Property(x => x.TypeVechicle)
                            .HasColumnType("int")
                            .IsRequired();
        }


    }
}
