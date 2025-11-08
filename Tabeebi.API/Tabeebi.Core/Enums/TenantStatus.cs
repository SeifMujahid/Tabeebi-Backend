using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

public enum TenantStatus
{
    [Display(Name = "Trial")]
    Trial = 0,

    [Display(Name = "Active")]
    Active = 1,

    [Display(Name = "Suspended")]
    Suspended = 2,

    [Display(Name = "Cancelled")]
    Cancelled = 3,

    [Display(Name = "Pending Verification")]
    PendingVerification = 4,

    [Display(Name = "Expired")]
    Expired = 5
}