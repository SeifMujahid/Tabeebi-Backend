using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabeebi.Domain.DTOs.Patient
{
    public class MedicalHistoryDto
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? Medications { get; set; }
        public string? FamilyMedicalHistory { get; set; }
        public string? ChronicConditions { get; set; }
        public string? PreviousSurgeries { get; set; }
    }
}