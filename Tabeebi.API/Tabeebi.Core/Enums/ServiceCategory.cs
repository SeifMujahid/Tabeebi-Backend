using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

/// <summary>
/// Represents the category of a medical service
/// </summary>
public enum ServiceCategory
{
    [Display(Name = "Consultation")]
    Consultation = 0,

    [Display(Name = "Diagnostic")]
    Diagnostic = 1,

    [Display(Name = "Procedure")]
    Procedure = 2,

    [Display(Name = "Treatment")]
    Treatment = 3,

    [Display(Name = "Vaccination")]
    Vaccination = 4,

    [Display(Name = "Screening")]
    Screening = 5,

    [Display(Name = "Therapy")]
    Therapy = 6,

    [Display(Name = "Laboratory")]
    Laboratory = 7,

    [Display(Name = "Imaging")]
    Imaging = 8,

    [Display(Name = "Preventive")]
    Preventive = 9,

    [Display(Name = "Other")]
    Other = 99
}