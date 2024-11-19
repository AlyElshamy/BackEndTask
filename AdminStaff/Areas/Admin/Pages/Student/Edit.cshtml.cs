using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminStaff.Data;
using AdminStaff.Entities;

namespace AdminStaff.Areas.Admin.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly AdminStaff.Data.AppDbContext _context;

        public EditModel(AdminStaff.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entities.Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =  await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            Student = student;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
