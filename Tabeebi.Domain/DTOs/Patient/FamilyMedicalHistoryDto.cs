using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Domain.DTOs.Patient
{
    /// <summary>
    /// DTO for family medical history
    /// </summary>
    public class FamilyMedicalHistoryDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Relationship { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Condition { get; set; } = string.Empty;

        [StringLength(20)]
        public string? IcdCode { get; set; }

        public int? AgeAtDiagnosis { get; set; }
        public bool IsGenetic { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
