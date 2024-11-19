using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdminStaff.Data;
using AdminStaff.Entities;
using AdminStaff.Interfaces;
using AdminStaff.Enums;
using Mono.TextTemplating;
using AdminStaff.Repositories;

namespace AdminStaff.Areas.Admin.Pages.Student
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public AddModel(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public List<NationalityEnum> myEnumList ;

        public IActionResult OnGet()
        {

            ViewData["Nationality"] = GetDirectionSelectList();
            return Page();
        }



        public static SelectList GetDirectionSelectList()
        {
            //fill enum list
             var directions = from NationalityEnum d in Enum.GetValues(typeof(NationalityEnum))
                              select new { ID = (int)d, Name = d.ToString() };

           
            return new SelectList(directions, "ID", "Name");
        }
         [BindProperty]
        public Entities.Student Student { get; set; }
        //for enum list
        //public IEnumerable<NationalityEnum> SelectNationality { get; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await unitOfWork.Students.AddAsync(Student);
            unitOfWork.Complete();
            return RedirectToPage("./Index");
        }
    }
}
