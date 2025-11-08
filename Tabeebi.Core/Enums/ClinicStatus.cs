using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

/// <summary>
/// Represents the status of a clinic profile
/// </summary>
public enum ClinicStatus
{
    [Display(Name = "Active")]
    Active = 0,

    [Display(Name = "Inactive")]
    Inactive = 1,

    [Display(Name = "Suspended")]
    Suspended = 2,

    [Display(Name = "Pending Verification")]
    PendingVerification = 3
}