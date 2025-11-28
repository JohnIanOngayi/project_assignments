using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_assignments.Models
{
    public class Project : Base
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Code cannot be longer than 30 characters")]
        public string? ProjectCode { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Name cannot be longer than 150 characters")]
        public string? ProjectName { get; set; }

        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        [ValidateNever]
        public Team? Team { get; set; }

        public bool IsBillable { get; set; } = false;

        [Required]
        [StringLength(10, ErrorMessage = "Status cannot be longer than 10 characters")]
        public string Status { get; set; } = "Active";

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Navigation property
        public ICollection<ProjectAssignment>? ProjectAssignments { get; set; }
    }
}
