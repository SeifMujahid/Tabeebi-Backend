using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Core.Entities
{
    public class MedicalHistory
    {

        /// <summary>
        /// Patient ID (reference to UserProfile with ProfileType = Patient)
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Doctor ID who last updated the medical history
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Blood type
        /// </summary>
        public string? BloodType { get; set; }

        /// <summary>
        /// Known allergies (include severity levels, e.g., "Peanuts - Severe")
        /// </summary>
        public string? Allergies { get; set; }

        /// <summary>
        /// Current medications
        /// </summary>
        public string? Medications { get; set; }

        /// <summary>
        /// Family medical history (include documentation for genetic conditions)
        /// </summary>
        public string? FamilyMedicalHistory { get; set; }

        /// <summary>
        /// Chronic conditions (for condition tracking; if present, follow-up scheduling is required but handled separately)
        /// </summary>
        public string? ChronicConditions { get; set; }

        /// <summary>
        /// Previous surgeries and procedures with dates
        /// </summary>
        public string? PreviousSurgeries { get; set; }

        /// <summary>
        /// Creation timestamp for audit trail
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last update timestamp for audit trail
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}