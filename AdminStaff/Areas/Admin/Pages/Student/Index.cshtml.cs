using AdminStaff.Entities;
using AdminStaff.Interfaces;
using AdminStaff.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminStaff.Areas.Admin.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork _unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            unitOfWork = _unitOfWork;
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public IEnumerable<Entities.Student> Students { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public async Task OnGetAsync()
        {
            Students = unitOfWork.Students.FindAll();
        }
        [BindProperty]
        public UserRoleVM UserRole { get; set; }
        public async Task<IActionResult> OnPostAsync(UserRoleVM userRole)
        {
            var olduser = await UserManager.FindByNameAsync(userRole.username);

            if (olduser == null)
                return NotFound();
            var isinrole = UserManager.IsInRoleAsync(olduser, userRole.RoleName);
            if (isinrole.Result!=true)
            {
                UserManager.RemoveFromRoleAsync(olduser, userRole.RoleName);
                UserManager.AddToRoleAsync(olduser, userRole.RoleName);
                unitOfWork.Complete();
            }
            return Redirect($"../Admin/student");
        }
        
    }
}
