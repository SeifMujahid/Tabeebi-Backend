using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

public enum Gender
{
    [Display(Name = "Male")]
    Male = 0,

    [Display(Name = "Female")]
    Female = 1,

    [Display(Name = "Other")]
    Other = 2,

    [Display(Name = "Prefer Not to Say")]
    PreferNotToSay = 3
}