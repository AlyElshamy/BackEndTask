using AdminStaff.Entities;
using AdminStaff.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdminStaff.Areas.Identity.Pages.Account
{
    public class ManageUserModel : PageModel
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public ManageUserModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
        [BindProperty]
        public UserRoleVM UserRole { get; set; }
        public async Task<IActionResult> OnPostChangeRole(UserRoleVM userRole)
        {
            var olduser = await UserManager.FindByNameAsync(userRole.username);

            if (olduser == null)
                return NotFound();

            var oldRoleName = RoleManager.Roles.SingleOrDefault().ToString();

            if (oldRoleName != userRole.RoleName)
            {
                UserManager.RemoveFromRoleAsync(olduser, oldRoleName);
                UserManager.AddToRoleAsync(olduser, userRole.RoleName);
            }

            return Page();
        }
        public void OnGet()
        {
        }
    }
}
