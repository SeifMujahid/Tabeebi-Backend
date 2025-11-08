using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

/// <summary>
/// Represents a user profile that can be a Patient, Doctor, Receptionist, Clinic Owner, or Super Admin
/// This entity replaces the deprecated Patient and User entities
/// </summary>
public class UserProfile : BaseTenantEntity
{
    /// <summary>
    /// First name of the user
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Profile image URL
    /// </summary>
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// JSON string containing user preferences
    /// </summary>
    public string? Preferences { get; set; }

    /// <summary>
    /// Last login date
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    /// <summary>
    /// Profile type determines role and which fields are relevant
    /// </summary>
    public UserProfileType ProfileType { get; set; }

    // Contact information (common to all profile types)
    /// <summary>
    /// Phone number
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Mobile phone number
    /// </summary>
    public string? MobileNumber { get; set; }

    /// <summary>
    /// Email address (synchronized with IdentityUser)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    // Patient specific fields
    /// <summary>
    /// Date of birth (for patients)
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gender (for patients)
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// National ID number
    /// </summary>
    public string? NationalId { get; set; }

    /// <summary>
    /// Passport number
    /// </summary>
    public string? PassportNumber { get; set; }

    /// <summary>
    /// Home address
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// City
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// State/Province
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Postal/ZIP code
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    // Medical information (for patients)
    /// <summary>
    /// Blood type
    /// </summary>
    public string? BloodType { get; set; }

    /// <summary>
    /// Known allergies
    /// </summary>
    public string? Allergies { get; set; }

    /// <summary>
    /// Current medications
    /// </summary>
    public string? Medications { get; set; }

    /// <summary>
    /// Medical history
    /// </summary>
    public string? MedicalHistory { get; set; }

    /// <summary>
    /// Family medical history
    /// </summary>
    public string? FamilyMedicalHistory { get; set; }

    // Emergency contact information
    /// <summary>
    /// Emergency contact name
    /// </summary>
    public string EmergencyContactName { get; set; } = string.Empty;

    /// <summary>
    /// Emergency contact phone
    /// </summary>
    public string EmergencyContactPhone { get; set; } = string.Empty;

    /// <summary>
    /// Relationship with emergency contact
    /// </summary>
    public string EmergencyContactRelationship { get; set; } = string.Empty;

    /// <summary>
    /// Emergency contact email
    /// </summary>
    public string? EmergencyContactEmail { get; set; }

    // Insurance information (for patients)
    /// <summary>
    /// Insurance provider name
    /// </summary>
    public string? InsuranceProvider { get; set; }

    /// <summary>
    /// Insurance policy number
    /// </summary>
    public string? InsurancePolicyNumber { get; set; }

    /// <summary>
    /// Insurance group number
    /// </summary>
    public string? InsuranceGroupNumber { get; set; }

    /// <summary>
    /// Insurance expiry date
    /// </summary>
    public DateTime? InsuranceExpiryDate { get; set; }

    // Patient preferences
    /// <summary>
    /// Preferred language for communication
    /// </summary>
    public string? PreferredLanguage { get; set; }

    /// <summary>
    /// Preferred doctor (if any)
    /// </summary>
    public string? PreferredDoctor { get; set; }

    /// <summary>
    /// Whether SMS reminders are enabled
    /// </summary>
    public bool SmsRemindersEnabled { get; set; } = true;

    /// <summary>
    /// Whether email reminders are enabled
    /// </summary>
    public bool EmailRemindersEnabled { get; set; } = true;

    // Patient specific metadata
    /// <summary>
    /// Patient registration date
    /// </summary>
    public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// How the patient was referred
    /// </summary>
    public string? ReferralSource { get; set; }

    /// <summary>
    /// Patient account status
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Doctor specific fields
    /// <summary>
    /// Medical license number
    /// </summary>
    public string? LicenseNumber { get; set; }

    /// <summary>
    /// Medical specialization
    /// </summary>
    public string? Specialization { get; set; }

    /// <summary>
    /// License expiry date
    /// </summary>
    public DateTime? LicenseExpiryDate { get; set; }

    /// <summary>
    /// Professional biography
    /// </summary>
    public string? Biography { get; set; }

    /// <summary>
    /// Years of experience
    /// </summary>
    public int? YearsOfExperience { get; set; }

    /// <summary>
    /// Education information
    /// </summary>
    public string? Education { get; set; }

    /// <summary>
    /// Professional certifications
    /// </summary>
    public string? Certifications { get; set; }

    // Staff specific fields (for Receptionist, Clinic Owner)
    /// <summary>
    /// Employee ID
    /// </summary>
    public string? EmployeeId { get; set; }

    /// <summary>
    /// Job title
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// Department
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// Hire date
    /// </summary>
    public DateTime? HireDate { get; set; }

    // Navigation properties
    public virtual Tenant Tenant { get; set; } = null!;
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    // Navigation collections based on profile type
    public virtual ICollection<Appointment> PatientAppointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
    public virtual ICollection<MedicalRecord> PatientMedicalRecords { get; set; } = new List<MedicalRecord>();
    public virtual ICollection<MedicalRecord> DoctorMedicalRecords { get; set; } = new List<MedicalRecord>();
    public virtual ICollection<Prescription> PatientPrescriptions { get; set; } = new List<Prescription>();
    public virtual ICollection<Prescription> DoctorPrescriptions { get; set; } = new List<Prescription>();
    public virtual ICollection<Payment> PatientPayments { get; set; } = new List<Payment>();
    public virtual ICollection<Invoice> PatientInvoices { get; set; } = new List<Invoice>();
    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
    public virtual ICollection<DoctorAnalytics> DoctorAnalytics { get; set; } = new List<DoctorAnalytics>();
    public virtual ICollection<PatientAnalytics> PatientAnalytics { get; set; } = new List<PatientAnalytics>();
}

