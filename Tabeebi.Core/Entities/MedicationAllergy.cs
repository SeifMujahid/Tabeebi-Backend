using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities
{
    /// <summary>
    /// Represents a medication allergy
    /// </summary>
    public class MedicationAllergy : BaseEntity
    {
        public Guid MedicalHistoryId { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; } = null!;

        public string MedicationName { get; set; } = string.Empty;
        public string? GenericName { get; set; }
        public AllergySeverity Severity { get; set; }
        public string Reaction { get; set; } = string.Empty;
        public DateTime? FirstReactionDate { get; set; }
        public bool IsVerified { get; set; } = false;
        public string? Notes { get; set; }
    }
}
