using AdminStaff.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminStaff.Data.Config
{
    public class FamilyMemberConfigurations : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.nationality)
                .HasConversion(
                     x => x.ToString(),
                     x => (NationalityEnum)Enum.Parse(typeof(NationalityEnum), x)
                );
            
            builder.Property(x => x.relatioship)
                .HasConversion(
                     x => x.ToString(),
                     x => (RelatioshipEnum)Enum.Parse(typeof(RelatioshipEnum), x)
                );
            builder.HasIndex(a => a.studentId)
                .IsUnique(false);

            builder.ToTable("FamilyMembers");
        }

        
    }
}
