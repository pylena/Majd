using Majd.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace Majd.Models
{
    public class Job
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Duties { get; set; }
        [Required]
        public string Qualification { get; set; }
        public string JobType { get; set; }
        [Required(ErrorMessage = "Please select work location.")]
        public string WorkLocation  { get; set; }

        public JobIndustry JobIndustry { get; set; }
        public string UserId { get; set; } // Required foreign key property
        public ApplicationUser User { get; set; } = null!; // Req reference!
        public string? Requirements { get; set; }
        public string? ReasonsToJoin { get; set; }
    }
}
