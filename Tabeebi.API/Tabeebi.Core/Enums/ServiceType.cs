using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

/// <summary>
/// Represents the type/delivery method of a medical service
/// </summary>
public enum ServiceType
{
    [Display(Name = "In Person")]
    InPerson = 0,

    [Display(Name = "Telemedicine")]
    Telemedicine = 1,

    [Display(Name = "Home Visit")]
    HomeVisit = 2,

    [Display(Name = "Laboratory")]
    Laboratory = 3,

    [Display(Name = "Imaging")]
    Imaging = 4,

    [Display(Name = "Procedure")]
    Procedure = 5,

    [Display(Name = "Consultation")]
    Consultation = 6,

    [Display(Name = "Follow Up")]
    FollowUp = 7,

    [Display(Name = "Emergency")]
    Emergency = 8,

    [Display(Name = "Other")]
    Other = 99
}