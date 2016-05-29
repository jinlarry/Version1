using System.ComponentModel.DataAnnotations;

namespace Version1.ViewModels.Volunteer
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name: ")]
        public string RoleName { get; set; }
    }
}
