using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Domain.DTOs.Patient
{
    /// <summary>
    /// DTO for chronic condition
    /// </summary>
    public class ChronicConditionDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ConditionName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? IcdCode { get; set; }

        [Required]
        public DateTime DiagnosisDate { get; set; }

        public int Status { get; set; }

        [StringLength(1000)]
        public string? TreatmentPlan { get; set; }

        public DateTime? FollowUpDate { get; set; }
        public bool RequiresFollowUp { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
