using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities
{

    /// <summary>
    /// Represents surgical history
    /// </summary>
    public class SurgicalHistory : BaseEntity
    {
        public Guid MedicalHistoryId { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; } = null!;

        public string ProcedureName { get; set; } = string.Empty;
        public string? ProcedureCode { get; set; }
        public DateTime ProcedureDate { get; set; }
        public string? Surgeon { get; set; }
        public string? Hospital { get; set; }
        public string? Complications { get; set; }
        public string? Outcome { get; set; }
        public string? Notes { get; set; }
    }

}
