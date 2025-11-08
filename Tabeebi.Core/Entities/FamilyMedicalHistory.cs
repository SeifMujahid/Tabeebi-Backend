using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities
{
    /// <summary>
    /// Represents family medical history
    /// </summary>
    public class FamilyMedicalHistory : BaseEntity
    {
        public Guid MedicalHistoryId { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; } = null!;

        public string Relationship { get; set; } = string.Empty; // Mother, Father, Sibling, etc.
        public string Condition { get; set; } = string.Empty;
        public string? IcdCode { get; set; }
        public int? AgeAtDiagnosis { get; set; }
        public bool IsGenetic { get; set; } = false;
        public string? Notes { get; set; }
    }
}
