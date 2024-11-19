using AdminStaff.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminStaff.ViewModels
{
    public class FamilyMemberVM
    {
        public string firstName { get; set; }
        public string lasttName { get; set; }
        public DateTime dateOfBirth { get; set; }
        //using enum in relations instead of craeating tables
        //public RelatioshipEnum relatioship { get; set; }
        //public NationalityEnum nationality { get; set; }
        public int studentId { get; set; }
        public int Relatioship { get; set; }
        public int Nationality { get; set; }
    }
}
