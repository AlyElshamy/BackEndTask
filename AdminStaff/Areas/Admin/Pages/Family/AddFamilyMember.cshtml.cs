using AdminStaff.Entities;
using AdminStaff.Interfaces;
using AdminStaff.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminStaff.Areas.Admin.Pages.Family
{
    public class AddFamilyMemberModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;

        public AddFamilyMemberModel(IUnitOfWork _unitOfWork, IMapper mapper)
        {
            unitOfWork = _unitOfWork;
            Mapper = mapper;
        }
        [BindProperty]
        public FamilyMemberVM Member { get; set; }
        public void OnGet(int id)
        {
            Member=new FamilyMemberVM{ studentId=id};
            //Member.studentId = id;
            ViewData["Nationality"] = ViewData["Nationality"] = GetNationalitySelectList();

            ViewData["Relationship"] = ViewData["Relationship"] = GetRelationsSelectList();
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var item=Mapper.Map<FamilyMember>(Member);
            await unitOfWork.FamilyMembers.AddAsync(item);
            unitOfWork.Complete();
            return Redirect($"../family/studentfamily?id={Member.studentId}");

        }

    }
}
