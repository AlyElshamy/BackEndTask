using AdminStaff.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AdminStaff.Data.Config
{
    public class StudentsConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.nationality)
                .HasConversion(
                     x => x.ToString(),
                     x => (NationalityEnum)Enum.Parse(typeof(NationalityEnum), x)
                );
            builder.Property(a => a.isSubmitted)
                .HasDefaultValue(false);

            builder.ToTable("Students");
        }

        
    }
}
