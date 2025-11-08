using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities
{
    /// <summary>
    /// Represents immunization record
    /// </summary>
    public class ImmunizationRecord : BaseEntity
    {
        public Guid MedicalHistoryId { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; } = null!;

        public string VaccineName { get; set; } = string.Empty;
        public string? VaccineCode { get; set; }
        public DateTime AdministrationDate { get; set; }
        public string? LotNumber { get; set; }
        public string? Manufacturer { get; set; }
        public string? Site { get; set; }
        public string? Route { get; set; }
        public string? AdministeredBy { get; set; }
        public DateTime? NextDueDate { get; set; }
        public string? Reaction { get; set; }
        public string? Notes { get; set; }
    }
}
