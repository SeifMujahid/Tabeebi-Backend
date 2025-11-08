using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

public class Prescription : BaseTenantEntity
{
    // Basic prescription information
    public string PrescriptionNumber { get; set; } = string.Empty; // Unique identifier for the prescription
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiryDate { get; set; }

    // Prescriber and Patient
    public Guid DoctorId { get; set; }
    public virtual UserProfile Doctor { get; set; } = null!;

    public Guid PatientId { get; set; }
    public virtual UserProfile Patient { get; set; } = null!;

    // Visit context
    public Guid? AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }
    public Guid? MedicalRecordId { get; set; }
    public virtual MedicalRecord? MedicalRecord { get; set; }

    // Prescription status
    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active;
    public string? Notes { get; set; }
    public string? Diagnosis { get; set; }

    // Digital signature
    public string? DigitalSignature { get; set; }
    public DateTime? SignedAt { get; set; }

    // Navigation properties
    public virtual ICollection<PrescriptionMedication> Medications { get; set; } = new List<PrescriptionMedication>();
    public virtual ICollection<PrescriptionRefill> Refills { get; set; } = new List<PrescriptionRefill>();
}

// Individual medications in a prescription
public class PrescriptionMedication : BaseEntity
{
    public Guid PrescriptionId { get; set; }
    public virtual Prescription Prescription { get; set; } = null!;

    // Medication details
    public string MedicationName { get; set; } = string.Empty;
    public string? GenericName { get; set; }
    public string? BrandName { get; set; }
    public string? NdcCode { get; set; } // National Drug Code
    public MedicationForm Form { get; set; } // Tablet, liquid, injection, etc.
    public string Strength { get; set; } = string.Empty; // e.g., "500mg", "10mg/ml"

    // Dosage instructions
    public string Dosage { get; set; } = string.Empty; // e.g., "1 tablet", "5ml"
    public string Frequency { get; set; } = string.Empty; // e.g., "twice daily", "every 8 hours"
    public string Route { get; set; } = string.Empty; // e.g., "oral", "intravenous", "topical"
    public string? Instructions { get; set; } // Additional instructions like "take with food"

    // Duration and quantity
    public int DurationDays { get; set; }
    public int TotalQuantity { get; set; }
    public string QuantityUnit { get; set; } = string.Empty; // e.g., "tablets", "ml", "capsules"

    // Refill information
    public int RefillsAllowed { get; set; } = 0;
    public int RefillsUsed { get; set; } = 0;
    public DateTime? NextRefillDate { get; set; }

    // Special instructions
    public bool IsPRN { get; set; } = false; // As needed (pro re nata)
    public string? PRNIndication { get; set; } // When to take if PRN
    public bool HasFoodRequirement { get; set; } = false;
    public FoodRequirement FoodRequirement { get; set; } = FoodRequirement.NoRestriction;

    // Pharmacy information
    public string? PreferredPharmacy { get; set; }
    public bool RequiresPriorAuthorization { get; set; } = false;
    public string? PriorAuthorizationCode { get; set; }

    // Status
    public PrescriptionMedicationStatus Status { get; set; } = PrescriptionMedicationStatus.Active;
    public DateTime? DispensedDate { get; set; }
    public string? DispensedBy { get; set; }
}

// Prescription refill tracking
public class PrescriptionRefill : BaseTenantEntity
{
    public Guid PrescriptionId { get; set; }
    public virtual Prescription Prescription { get; set; } = null!;

    public Guid PrescriptionMedicationId { get; set; }
    public virtual PrescriptionMedication Medication { get; set; } = null!;

    // Refill details
    public int RefillNumber { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedDate { get; set; }
    public RefillStatus Status { get; set; } = RefillStatus.Requested;

    // Processing information
    public string? PharmacyName { get; set; }
    public string? ProcessedBy { get; set; }
    public int QuantityDispensed { get; set; }
    public string? Notes { get; set; }
}

// Enums for prescription management
public enum PrescriptionStatus
{
    Draft = 0,        // Being written by doctor
    Active = 1,       // Currently valid
    Expired = 2,      // Past expiry date
    Cancelled = 3,    // Cancelled by prescriber
    Completed = 4,    // All medications dispensed
    Suspended = 5     // Temporarily suspended
}

public enum PrescriptionMedicationStatus
{
    Active = 0,
    Discontinued = 1,
    Completed = 2,
    OnHold = 3
}

public enum MedicationForm
{
    Tablet = 0,
    Capsule = 1,
    Liquid = 2,
    Injection = 3,
    Topical = 4,
    Inhaler = 5,
    Patch = 6,
    Drops = 7,
    Spray = 8,
    Powder = 9,
    Suppository = 10,
    Other = 99
}

public enum FoodRequirement
{
    NoRestriction = 0,
    WithFood = 1,
    WithoutFood = 2,
    WithMeals = 3
}

public enum RefillStatus
{
    Requested = 0,
    Approved = 1,
    Processed = 2,
    Denied = 3,
    Expired = 4
}