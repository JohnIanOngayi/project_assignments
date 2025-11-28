using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_assignments.Models
{
    public class ProjectAssignment : Base, IValidatableObject
    {
        [Key]
        public int AssignmentId { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [ValidateNever]
        public Project? Project { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee? Employee { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Role cannot be longer than 100 characters")]
        public string? RoleOnProject { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        [Range(0.01, 100, ErrorMessage = "Allocation percent must be between 0 and 100")]
        public decimal AllocationPercent { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Status cannot be longer than 10 characters")]
        public string Status { get; set; } = "Active";

        // Custom validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "End date must be greater than or equal to start date",
                    [nameof(StartDate), nameof(EndDate)]
                );
            }
        }
    }
}
