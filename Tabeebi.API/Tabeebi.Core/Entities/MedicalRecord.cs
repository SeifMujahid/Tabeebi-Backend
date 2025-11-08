using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities;

public class MedicalRecord : BaseTenantEntity
{
    public virtual Tenant Tenant { get; set; } = null!;

    // Patient and Doctor references - updated to use UserProfile
    public Guid PatientId { get; set; }
    public virtual UserProfile Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public virtual UserProfile Doctor { get; set; } = null!;

    public Guid AppointmentId { get; set; }
    public virtual Appointment Appointment { get; set; } = null!;

    // Chief complaint and history
    public string ChiefComplaint { get; set; } = string.Empty;
    public string? HistoryOfPresentIllness { get; set; }
    public string? PastMedicalHistory { get; set; }
    public string? FamilyHistory { get; set; }
    public string? SocialHistory { get; set; }

    // Examination findings
    public string? PhysicalExamination { get; set; }
    public string? VitalSigns { get; set; }
    public decimal? Height { get; set; } // in cm
    public decimal? Weight { get; set; } // in kg
    public decimal? BloodPressureSystolic { get; set; }
    public decimal? BloodPressureDiastolic { get; set; }
    public decimal? HeartRate { get; set; }
    public decimal? Temperature { get; set; } // in Celsius
    public decimal? OxygenSaturation { get; set; } // in percentage

    // Assessment and plan
    public string? Assessment { get; set; }
    public string? Diagnosis { get; set; } // Structured diagnosis field
    public string? TreatmentPlan { get; set; }

    // Orders - simplified from string fields
    public string? LabOrders { get; set; }
    public string? ImagingOrders { get; set; }
    public string? Referrals { get; set; }

    // Follow-up
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpInstructions { get; set; }

    // Digital signature
    public string? DigitalSignature { get; set; }
    public DateTime? SignedDate { get; set; }
    public bool IsSigned { get; set; } = false;

    // Status and completion
    public MedicalRecordStatus Status { get; set; } = MedicalRecordStatus.Draft;
    public DateTime? CompletedDate { get; set; }
    public string? CompletedBy { get; set; }

    // Navigation properties - structured relationships
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
    public virtual ICollection<MedicalRecordAttachment> Attachments { get; set; } = new List<MedicalRecordAttachment>();

    // Audit trail
    public string? PreviousVersionId { get; set; }
    public bool IsCorrection { get; set; } = false;
    public string? CorrectionReason { get; set; }
}

// Structured diagnosis entity for medical records
public class Diagnosis : BaseEntity
{
    public Guid MedicalRecordId { get; set; }
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    // Diagnosis details
    public string Code { get; set; } = string.Empty; // ICD-10 code
    public string Description { get; set; } = string.Empty;
    public DiagnosisType Type { get; set; }
    public DiagnosisStatus Status { get; set; } = DiagnosisStatus.Active;
    public DateTime? OnsetDate { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public bool IsChronic { get; set; } = false;
    public string? ClinicalNotes { get; set; }

    // Diagnostic certainty
    public DiagnosisCertainty Certainty { get; set; } = DiagnosisCertainty.Confirmed;
    public string? DifferentialDiagnosis { get; set; } // Alternative diagnoses considered
}

// Medical record attachments
public class MedicalRecordAttachment : BaseTenantEntity
{
    public Guid MedicalRecordId { get; set; }
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    // Attachment details
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? PublicUrl { get; set; }

    // Attachment metadata
    public AttachmentType Type { get; set; }
    public string? Description { get; set; }
    public DateTime? DateTaken { get; set; }
    public string? UploadedBy { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

// Enums for medical records
public enum MedicalRecordStatus
{
    Draft = 0,
    InProgress = 1,
    Completed = 2,
    Signed = 3,
    Amended = 4,
    Archived = 5
}

public enum DiagnosisType
{
    Primary = 0,
    Secondary = 1,
    Admitting = 2,
    Discharge = 3,
    Working = 4,
    Final = 5
}

public enum DiagnosisStatus
{
    Active = 0,
    Resolved = 1,
    Chronic = 2,
    Recurrent = 3,
    InRemission = 4
}

public enum DiagnosisCertainty
{
    Confirmed = 0,
    Probable = 1,
    Possible = 2,
    RuleOut = 3,
    Differential = 4
}

public enum AttachmentType
{
    Document = 0,
    Image = 1,
    LabResult = 2,
    ImagingReport = 3,
    Video = 4,
    Audio = 5,
    Other = 99
}