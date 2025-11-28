using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace project_assignments.Models
{
    public class Department : Base
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Code cannot be longer than 20 characters")]
        public string? DepartmentCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string? DepartmentName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Status cannot be longer than 10 characters")]
        public string Status { get; set; } = "Active";

        // Navigation property
        public ICollection<Team>? Teams { get; set; }
    }
}
