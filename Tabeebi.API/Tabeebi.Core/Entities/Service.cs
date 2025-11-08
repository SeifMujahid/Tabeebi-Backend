using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

/// <summary>
/// Represents a medical service offered by the clinic
/// </summary>
public class Service : BaseTenantEntity
{
    /// <summary>
    /// Service name (e.g., "General Consultation", "Blood Test", "X-Ray")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Service code for billing and reporting
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the service
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Service category for organization and reporting
    /// </summary>
    public ServiceCategory Category { get; set; }

    /// <summary>
    /// Type of service
    /// </summary>
    public ServiceType Type { get; set; }

    /// <summary>
    /// Standard duration for this service
    /// </summary>
    public TimeSpan DefaultDuration { get; set; }

    /// <summary>
    /// Base price for the service
    /// </summary>
    public decimal BasePrice { get; set; }

    /// <summary>
    /// Currency code (e.g., USD, EUR)
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Whether this service requires a doctor
    /// </summary>
    public bool RequiresDoctor { get; set; } = true;

    /// <summary>
    /// Whether this service requires a room
    /// </summary>
    public bool RequiresRoom { get; set; } = false;

    /// <summary>
    /// Whether this service can be booked online
    /// </summary>
    public bool AllowOnlineBooking { get; set; } = true;

    /// <summary>
    /// Minimum notice required for booking/cancellation
    /// </summary>
    public TimeSpan MinimumNotice { get; set; } = TimeSpan.FromHours(2);

    /// <summary>
    /// Whether insurance can be billed for this service
    /// </summary>
    public bool IsInsuranceBillable { get; set; } = true;

    /// <summary>
    /// CPT code for medical billing
    /// </summary>
    public string? CptCode { get; set; }

    /// <summary>
    /// ICD-10 codes commonly associated with this service
    /// </summary>
    public string? AssociatedDiagnosisCodes { get; set; }

    /// <summary>
    /// Special requirements or preparations needed
    /// </summary>
    public string? Requirements { get; set; }

    /// <summary>
    /// Service status
    /// </summary>
    public ServiceStatus Status { get; set; } = ServiceStatus.Active;

    /// <summary>
    /// Sort order for display
    /// </summary>
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// Whether this is a popular service
    /// </summary>
    public bool IsPopular { get; set; } = false;

    /// <summary>
    /// Service image or icon URL
    /// </summary>
    public string? ImageUrl { get; set; }

    // Legacy compatibility properties
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; } = 30;
    public string? IcdCode { get; set; } // International Classification of Diseases
    public bool RequiresPreAuthorization { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public string? PreparationInstructions { get; set; }
    public string? PostProcedureInstructions { get; set; }

    // Pricing by doctor role
    public decimal? DoctorPrice { get; set; }
    public decimal? SpecialistPrice { get; set; }

    // Resource requirements
    public string? RequiredEquipment { get; set; }
    public string? RequiredRoom { get; set; }
    public int? RequiredStaffCount { get; set; }

    // Status
    public bool IsAvailableOnline { get; set; } = true;
    public int? MaxConcurrentAppointments { get; set; } = 1;

    // Navigation properties
    public virtual ClinicProfile Clinic { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<PaymentItem> PaymentItems { get; set; } = new List<PaymentItem>();
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    public virtual ICollection<ServiceAnalytics> Analytics { get; set; } = new List<ServiceAnalytics>();
}

