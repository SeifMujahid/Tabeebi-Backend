using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Domain.DTOs.Patient
{
    /// <summary>
    /// DTO for surgical history
    /// </summary>
    public class SurgicalHistoryDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ProcedureName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ProcedureCode { get; set; }

        [Required]
        public DateTime ProcedureDate { get; set; }

        [StringLength(200)]
        public string? Surgeon { get; set; }

        [StringLength(200)]
        public string? Hospital { get; set; }

        [StringLength(1000)]
        public string? Complications { get; set; }

        [StringLength(500)]
        public string? Outcome { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
