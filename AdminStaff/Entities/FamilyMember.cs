using AdminStaff.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminStaff.Entities
{
    public class FamilyMember
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lasttName { get; set; }
        public DateTime dateOfBirth { get; set; }
        //using enum in relations instead of craeating tables
        public RelatioshipEnum relatioship { get; set; }
        public NationalityEnum nationality { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? student { get; set; }
        public int studentId { get; set; }
       

        //[ForeignKey("RelatioshipId")]
        //public virtual Relatioship relatioship { get; set; }
        //public int RelatioshipId { get; set; }

        // [ForeignKey("NationalityId")]
        //public virtual Nationality Nationality { get; set; }
        //public int NationalityId { get; set; }
    }
   
}
