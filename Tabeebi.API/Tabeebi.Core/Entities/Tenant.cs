using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

/// <summary>
/// Represents a tenant in the multi-tenant system
/// </summary>
public class Tenant : BaseEntity
{
    /// <summary>
    /// Tenant name (clinic or organization name)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Unique subdomain for the tenant
    /// </summary>
    public string Subdomain { get; set; } = string.Empty;

    /// <summary>
    /// Tenant description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Tenant subscription status
    /// </summary>
    public TenantStatus Status { get; set; } = TenantStatus.Trial;

    /// <summary>
    /// Trial period end date
    /// </summary>
    public DateTime TrialEndDate { get; set; }

    /// <summary>
    /// Subscription end date
    /// </summary>
    public DateTime? SubscriptionEndDate { get; set; }

    /// <summary>
    /// Subscription plan name
    /// </summary>
    public string? SubscriptionPlan { get; set; }

    /// <summary>
    /// Maximum allowed users
    /// </summary>
    public int MaxUsers { get; set; } = 10;

    /// <summary>
    /// Maximum storage in GB
    /// </summary>
    public int MaxStorageGB { get; set; } = 5;

    /// <summary>
    /// Contact email for tenant
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// Contact phone for tenant
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// Tenant address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Tenant city
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Tenant country
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Tenant postal code
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Whether tenant is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Tenant logo URL
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// UI theme settings
    /// </summary>
    public string? Theme { get; set; }

    /// <summary>
    /// Additional tenant settings
    /// </summary>
    public Dictionary<string, string>? Settings { get; set; }

    /// <summary>
    /// Time zone for the tenant
    /// </summary>
    public string? TimeZone { get; set; }

    /// <summary>
    /// Default currency for the tenant
    /// </summary>
    public string DefaultCurrency { get; set; } = "USD";

    /// <summary>
    /// Date format preference
    /// </summary>
    public string? DateFormat { get; set; }

    /// <summary>
    /// Whether the tenant has been verified
    /// </summary>
    public bool IsVerified { get; set; } = false;

    /// <summary>
    /// Date when tenant was verified
    /// </summary>
    public DateTime? VerifiedDate { get; set; }

    /// <summary>
    /// Date when tenant was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Database schema name for this tenant
    /// </summary>
    public string? SchemaName { get; set; }

    // Navigation properties
    public virtual ICollection<IdentityUser> Users { get; set; } = new List<IdentityUser>();
    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
    public virtual ClinicProfile? ClinicProfile { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<InsuranceProvider> InsuranceProviders { get; set; } = new List<InsuranceProvider>();
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    public virtual ICollection<DailyAnalytics> DailyAnalytics { get; set; } = new List<DailyAnalytics>();
}

