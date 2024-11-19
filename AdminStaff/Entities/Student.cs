using AdminStaff.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminStaff.Entities
{
    public class Student
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lasttName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isSubmitted { get; set; }
        public NationalityEnum nationality { get; set; }

        //to use tables instead of enums
        //[ForeignKey("NationalityId")]
        //public virtual Nationality? nationality { get; set; }
        //public int NationalityId { get; set; }
        public virtual ICollection<FamilyMember> familyMembers { get; set; } = new List<FamilyMember>();

    }
    
}
