using BusinessInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessInfo.Persistence.Configurations
{
    public class IssuerTypeConfiguration : BaseEntityTypeConfiguration<Issuer>
    {
        public override void Configure(EntityTypeBuilder<Issuer> builder)
        {
            builder.ToTable(nameof(Issuer));
            builder.Property(x => x.Id)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.HasOne(i => i.Companies)
               .WithOne()
               .HasForeignKey<Issuer>(i => i.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);



        }


    }

}
