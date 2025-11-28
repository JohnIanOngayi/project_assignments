using System.ComponentModel.DataAnnotations;

namespace project_assignments.Models
{
    public class Employee : Base
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Code cannot be longer than 30 characters")]
        public string? EmployeeCode { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Name cannot be longer than 150 characters")]
        public string? EmployeeName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150, ErrorMessage = "Email cannot be longer than 150 characters")]
        public string? EmployeeEmail { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation property
        public ICollection<ProjectAssignment>? ProjectAssignments { get; set; }
    }
}
