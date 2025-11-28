using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace project_assignments.Models
{
    [Index(nameof(DepartmentId), nameof(TeamName), IsUnique = true, Name = "UQ_Teams_Department_TeamName")]
    public class Team : Base
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Code cannot be longer than 20 characters")]
        public string? TeamCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string? TeamName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Status cannot be longer than 10 characters")]
        public string Status { get; set; } = "Active";

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public Department? Department { get; set; }

        // Navigation property
        public ICollection<Project>? Projects { get; set; }
    }
}
