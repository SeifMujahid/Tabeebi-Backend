using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Domain.DTOs.Patient
{

    /// <summary>
    /// DTO for immunization record
    /// </summary>
    public class ImmunizationRecordDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string VaccineName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? VaccineCode { get; set; }

        [Required]
        public DateTime AdministrationDate { get; set; }

        [StringLength(50)]
        public string? LotNumber { get; set; }

        [StringLength(100)]
        public string? Manufacturer { get; set; }

        [StringLength(50)]
        public string? Site { get; set; }

        [StringLength(50)]
        public string? Route { get; set; }

        [StringLength(200)]
        public string? AdministeredBy { get; set; }

        public DateTime? NextDueDate { get; set; }

        [StringLength(500)]
        public string? Reaction { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
