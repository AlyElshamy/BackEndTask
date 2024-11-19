using System.ComponentModel.DataAnnotations;

namespace AdminStaff.ViewModels
{
    public class RoleFormVM
    {
            [Required, StringLength(256)]
            public string Name { get; set; }
    }
}
