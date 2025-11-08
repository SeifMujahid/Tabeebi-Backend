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
    /// Represents a chronic condition
    /// </summary>
    public class ChronicCondition : BaseEntity
    {
        public Guid MedicalHistoryId { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; } = null!;

        public string ConditionName { get; set; } = string.Empty;
        public string? IcdCode { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public ConditionStatus Status { get; set; } = ConditionStatus.Active;
        public string? TreatmentPlan { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public bool RequiresFollowUp { get; set; } = false;
        public string? Notes { get; set; }
    }

}
