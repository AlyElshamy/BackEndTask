

namespace AdminStaff.Entities
{
    public class ApplicationUser:Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
