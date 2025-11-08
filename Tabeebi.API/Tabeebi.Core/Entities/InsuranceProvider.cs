using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

/// <summary>
/// Represents an insurance provider company
/// </summary>
public class InsuranceProvider : BaseTenantEntity
{
    /// <summary>
    /// Insurance provider name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Unique provider code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Provider description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Contact email
    /// </summary>
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    /// Contact phone number
    /// </summary>
    public string ContactPhone { get; set; } = string.Empty;

    /// <summary>
    /// Company website
    /// </summary>
    public string Website { get; set; } = string.Empty;

    /// <summary>
    /// Company headquarters address
    /// </summary>
    public Address Headquarters { get; set; } = new Address();

    /// <summary>
    /// Insurance provider type
    /// </summary>
    public InsuranceProviderType ProviderType { get; set; }

    /// <summary>
    /// Whether this provider is in-network
    /// </summary>
    public bool IsInNetwork { get; set; } = true;

    /// <summary>
    /// Network type (PPO, HMO, EPO, etc.)
    /// </summary>
    public string? NetworkType { get; set; }

    /// <summary>
    /// Standard copay amount
    /// </summary>
    public decimal? CopayAmount { get; set; }

    /// <summary>
    /// Standard deductible amount
    /// </summary>
    public decimal? DeductibleAmount { get; set; }

    /// <summary>
    /// Coinsurance percentage
    /// </summary>
    public decimal? CoinsurancePercentage { get; set; }

    /// <summary>
    /// Covered services (JSON array of service codes)
    /// </summary>
    public string? CoveredServices { get; set; }

    /// <summary>
    /// Service exclusions
    /// </summary>
    public string? Exclusions { get; set; }

    /// <summary>
    /// Pre-authorization requirements
    /// </summary>
    public string? PreAuthorizationRequirements { get; set; }

    /// <summary>
    /// Days allowed for claim submission
    /// </summary>
    public int ClaimSubmissionDays { get; set; } = 30;

    /// <summary>
    /// Claim submission method
    /// </summary>
    public string? ClaimSubmissionMethod { get; set; }

    /// <summary>
    /// Payer ID for electronic claims
    /// </summary>
    public string? PayerId { get; set; }

    /// <summary>
    /// Provider status
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date when provider was verified
    /// </summary>
    public DateTime? VerificationDate { get; set; }

    /// <summary>
    /// Who verified the provider
    /// </summary>
    public string? VerifiedBy { get; set; }

    /// <summary>
    /// Last updated date
    /// </summary>
    public DateTime? LastUpdatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Provider logo URL
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// Sort order for display
    /// </summary>
    public int SortOrder { get; set; } = 0;

    // Navigation properties
    public virtual ICollection<ClinicProfile> AcceptedByClinics { get; set; } = new List<ClinicProfile>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

/// <summary>
/// Represents a physical address
/// </summary>
public class Address
{
    /// <summary>
    /// Street address line 1
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Street address line 2
    /// </summary>
    public string? StreetLine2 { get; set; }

    /// <summary>
    /// City name
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// State/Province
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Country
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Postal/ZIP code
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// Address coordinates (optional)
    /// </summary>
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}

