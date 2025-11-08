using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

/// <summary>
/// Represents the status of a medical service
/// </summary>
public enum ServiceStatus
{
    [Display(Name = "Active")]
    Active = 0,

    [Display(Name = "Inactive")]
    Inactive = 1,

    [Display(Name = "Seasonal")]
    Seasonal = 2,

    [Display(Name = "Discontinued")]
    Discontinued = 3,

    [Display(Name = "Coming Soon")]
    ComingSoon = 4
}