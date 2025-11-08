using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities;

public class ClinicProfile : BaseTenantEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? LogoUrl { get; set; }
    public string? TimeZone { get; set; } = "UTC";

    // Operating hours
    public string? OperatingHours { get; set; }
    public string? WeekdaysHours { get; set; }
    public string? SaturdayHours { get; set; }
    public string? SundayHours { get; set; }

    // Emergency information
    public string EmergencyPhone { get; set; } = string.Empty;
    public string EmergencyEmail { get; set; } = string.Empty;

    // Facility information
    public int? TotalRooms { get; set; }
    public int? ParkingSpaces { get; set; }
    public bool HasWheelchairAccess { get; set; } = true;
    public string? AdditionalFacilities { get; set; }

    // Status
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedDate { get; set; }
    public string? VerifiedBy { get; set; }

    // Navigation properties
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    public virtual ICollection<InsuranceProvider> AcceptedInsurances { get; set; } = new List<InsuranceProvider>();
}