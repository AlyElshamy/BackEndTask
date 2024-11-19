using AdminStaff.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace AdminStaff.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        //public DbSet<Nationality> Nationalities { get; set; }
        //public DbSet<Relatioship> Relatioships { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            //modelBuilder.Entity<ApplicationUser>().ToTable("Users", "security");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "ADMIN".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", Name = "Registerar", NormalizedName = "REGISTERAR".ToUpper() });
        }
    }
}
