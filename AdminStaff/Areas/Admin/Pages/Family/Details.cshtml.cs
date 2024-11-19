using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminStaff.Data;
using AdminStaff.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AdminStaff.Repositories;
using AdminStaff.Interfaces;

namespace AdminStaff.Areas.Admin.Pages.Family
{
    public class DetailsModel : PageModel
    {
        private readonly AdminStaff.Data.AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        public DetailsModel(AdminStaff.Data.AppDbContext context,IUnitOfWork unitofwork)
        {
            _context = context;
            unitOfWork = unitofwork;
        }
        [BindProperty]
        public Task<FamilyMember> FamilyMember { get; set; } = default!;
      

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FamilyMember = unitOfWork.FamilyMembers.FindAsync(a => a.ID == id, new []{ "student" }); ;
            
            //FamilyMember = await _context.FamilyMembers.Include(a=>a.student)
            //    .FirstOrDefaultAsync(m => m.ID == id);
           
            return Page();
        }
    }
}
