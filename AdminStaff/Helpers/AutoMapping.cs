using AdminStaff.Areas.Admin.Pages.Family;
using AdminStaff.Entities;
using AdminStaff.ViewModels;
using AutoMapper;

namespace AdminStaff.Helpers
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<FamilyMemberVM, FamilyMember>();
        }
    }
}
