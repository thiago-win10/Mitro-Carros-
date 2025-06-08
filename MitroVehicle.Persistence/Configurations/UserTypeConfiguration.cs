using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Persistence.Configurations
{
    public class UserTypeConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.Property(x => x.Email)
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            builder.Property(x => x.Password)
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            builder.Property(x => x.Role)
                            .HasColumnType("varchar(255)")
                            .IsRequired(false);

            builder.Property(x => x.Name)
               .HasColumnType("varchar(255)")
               .IsRequired(false);


            builder.Property(x => x.Document)
               .HasColumnType("varchar(255)")
               .IsRequired(false);

            builder.Property(x => x.Phone)
               .HasColumnType("varchar(255)")
               .IsRequired(false);

            builder.Property(x => x.Age)
               .IsRequired();

        }
    }
}

