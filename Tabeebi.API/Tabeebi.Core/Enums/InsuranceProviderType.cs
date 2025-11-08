using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

/// <summary>
/// Represents the type of insurance provider
/// </summary>
public enum InsuranceProviderType
{
    [Display(Name = "Private")]
    Private = 0,

    [Display(Name = "Public")]
    Public = 1,

    [Display(Name = "Government")]
    Government = 2,

    [Display(Name = "Military")]
    Military = 3,

    [Display(Name = "Workers Compensation")]
    WorkersComp = 4,

    [Display(Name = "Auto Insurance")]
    Auto = 5,

    [Display(Name = "Other")]
    Other = 99
}