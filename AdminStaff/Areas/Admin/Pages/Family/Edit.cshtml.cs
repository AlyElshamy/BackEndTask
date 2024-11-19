using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminStaff.Entities;
using AdminStaff.Repositories;
using AdminStaff.Data;

namespace AdminStaff.Areas.Admin.Pages.Family
{
    public class EditModel : PageModel
    {
        private readonly AdminStaff.Data.AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FamilyMember FamilyMember { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familymember = await _context.FamilyMembers.FindAsync(id);
            if (familymember == null)
            {
                return NotFound();
            }

            FamilyMember=new FamilyMember(){ID= familymember.ID
                ,dateOfBirth= familymember.dateOfBirth,
                firstName= familymember.firstName
                ,lasttName= familymember.lasttName
                ,nationality= familymember.nationality,
                relatioship= familymember.relatioship,
                student= familymember.student,   
                studentId= familymember.studentId,
            };
            ViewData["Nationalities"] = GetNationalitySelectList();

             ViewData["Relationships"] = GetRelationsSelectList();
        
        ViewData["students"] = new SelectList(_context.Students.ToList(), "ID", "firstName");
            return Page();
        }
        public static SelectList GetNationalitySelectList()
        {
            //fill enum list
            var directions = from NationalityEnum d in Enum.GetValues(typeof(NationalityEnum))
                             select new { ID = (int)d, Name = d.ToString() };
            return new SelectList(directions, "ID", "Name");
        }
        public static SelectList GetRelationsSelectList()
        {
            //fill enum list
            var directions = from RelatioshipEnum d in Enum.GetValues(typeof(RelatioshipEnum))
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

            _context.FamilyMembers.Update(FamilyMember);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyMemberExists(FamilyMember.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect($"../Family/studentfamily?id={FamilyMember.studentId}");
        }

        private bool FamilyMemberExists(int id)
        {
            if (_context.FamilyMembers.FindAsync( id) !=null)
            {
                return true ;
            }
            else { return false; }
            
        }
    }
}
