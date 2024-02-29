using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Majd.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please enter your organization name"), MaxLength(150)]
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "Please enter your career sectore name"), MaxLength(100)]
        public string CareerSectore { get; set; }
        [Required(ErrorMessage = "Please enter your commercial register ")]
        public int CommercialRegister { get; set; }
        [Required]
        public bool IsAproved { get; set; } = false;
        [MaxLength(250)]
        public string? RejectionReason { get; set; }
    }
}
