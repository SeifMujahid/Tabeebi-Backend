using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Domain.DTOs.Patient
{
    /// <summary>
    /// DTO for medication allergy
    /// </summary>
    public class MedicationAllergyDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string MedicationName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? GenericName { get; set; }

        [Required]
        public int Severity { get; set; }

        [Required]
        [StringLength(500)]
        public string Reaction { get; set; } = string.Empty;

        public DateTime? FirstReactionDate { get; set; }
        public bool IsVerified { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
