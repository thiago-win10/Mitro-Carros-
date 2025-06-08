using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Persistence.Configurations
{
    public class ClientTypeConfiguration : BaseEntityTypeConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.HasOne(x => x.User)
              .WithOne(x => x.Client)
              .HasForeignKey<Client>(x => x.UserId)
                  .IsRequired(true)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IsAdmin).IsRequired().HasDefaultValue(false);
        }
    }
}
