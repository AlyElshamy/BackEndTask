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

namespace AdminStaff.Areas.Admin.Pages.Student
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public DetailsModel(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
       

        public Task<Entities.Student> Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = unitOfWork.Students.FindAsync(a => a.ID == id); ;
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }
            return Page();
        }
    }
}
