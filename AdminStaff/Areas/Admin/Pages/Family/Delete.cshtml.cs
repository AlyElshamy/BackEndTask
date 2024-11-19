using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminStaff.Data;
using AdminStaff.Entities;
using AdminStaff.Interfaces;
using AutoMapper.Execution;

namespace AdminStaff.Areas.Admin.Pages.Family
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public FamilyMember FamilyMember { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familymember = await unitOfWork.FamilyMembers.FindAsync(m => m.ID == id);

            if (familymember == null)
            {
                return NotFound();
            }
            else
            {
                FamilyMember = familymember;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familymember = await unitOfWork.FamilyMembers.FindAsync(id);
            if (familymember != null)
            {
                FamilyMember = familymember;
                unitOfWork.FamilyMembers.Delete(FamilyMember);
                unitOfWork.Complete();
            }

            return Redirect($"../family/studentfamily?id={familymember.studentId}");
        }
    }
}
