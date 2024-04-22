using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Majd.Models
{
    public class ApplicationUser : IdentityUser


    {
        //Employer
        [Required(ErrorMessage = "Please enter your organization name"), MaxLength(150)]
        public string OrganizationName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your career sectore name"), MaxLength(100)]
        public string CareerSectore { get; set; }
        [Required(ErrorMessage = "Please enter your commercial register ")]
        public int CommercialRegister { get; set; }
        [Required]
        public bool IsAproved { get; set; } = false;
        [MaxLength(250)]
        public string? RejectionReason { get; set; }
        [MaxLength(400)]
        public string? CompanyDescription { get; set; }
        public string? Headquarter { get; set; }
        public string? UrlLocation { get; set; }
        public ICollection<Job> Jobs { get; } = new List<Job>();

        //Alumni
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AcademicDegree { get; set; } = string.Empty;
        //public string? About {  get; set; }
        public int GradYear { get; set; }
        [Range(1.0, 5.0)]
        public double GPA { get; set; }
        [MaxLength(600)]
        public string EductionDescription { get; set; } = string.Empty;
        public string? MajorId { get; set; }
        public Major? Major { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
