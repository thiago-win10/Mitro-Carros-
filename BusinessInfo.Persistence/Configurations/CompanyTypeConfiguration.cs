using BusinessInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessInfo.Persistence.Configurations
{
    public class CompanyTypeConfiguration : BaseEntityTypeConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company));
            builder.Property(x => x.Name)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.Cnpj)
                            .HasColumnType("varchar(18)")
                            .IsRequired();

            builder.Property(x => x.Segment)
                            .HasColumnType("varchar(255)")
                            .IsRequired();

            builder.OwnsOne(c => c.Address, addr =>
            {
                addr.Property(a => a.Street).IsRequired().HasMaxLength(100);
                addr.Property(a => a.Number).IsRequired().HasMaxLength(10);
                addr.Property(a => a.Neighborhood).IsRequired().HasMaxLength(60);
                addr.Property(a => a.City).IsRequired().HasMaxLength(60);
                addr.Property(a => a.State).IsRequired().HasMaxLength(2);
                addr.Property(a => a.ZipCode).IsRequired().HasMaxLength(9);

                addr.ToTable("CompanyAddresses"); // opcional
            });

            builder.OwnsOne(c => c.ContactPerson, p =>
            {
                p.Property(x => x.FullName).IsRequired().HasMaxLength(100);
                p.Property(x => x.Occupation).HasMaxLength(60);
                p.Property(x => x.Email).IsRequired().HasMaxLength(100);
                p.Property(x => x.Phone).HasMaxLength(20);

                p.ToTable("CompanyContacts"); // opcional
            });



        }


    }
}