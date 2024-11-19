using AdminStaff.Data;
using AdminStaff.Entities;
using AdminStaff.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdminStaff.Areas.Admin.Pages.Family
{
    public class StudentFamilyModel : PageModel
    {
        private readonly AppDbContext Context;

        public StudentFamilyModel(AppDbContext context)
        {
            Context = context;
        }
        [BindProperty]
        public IEnumerable<FamilyMember> FamilyMembers { get; set; }
        //public AppDbContext Context { get; }

        public async Task<IActionResult> OnGet(int id)
        {
            FamilyMembers=Context.FamilyMembers.Include(a=>a.student).Where(a=>a.studentId==id).ToList();
                if (FamilyMembers.Count() == 0 )
                return Redirect($"../Family/Addfamilymember?id={id}");
                else
                return Page();
        }

    }
}
