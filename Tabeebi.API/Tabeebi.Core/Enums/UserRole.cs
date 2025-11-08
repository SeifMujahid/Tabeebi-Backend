using System.ComponentModel.DataAnnotations;

namespace Tabeebi.Core.Enums;

public enum UserRole
{
    [Display(Name = "Super Admin")]
    SuperAdmin = 0,

    [Display(Name = "Clinic Owner")]
    ClinicOwner = 1,

    [Display(Name = "Doctor")]
    Doctor = 2,

    [Display(Name = "Receptionist")]
    Receptionist = 3,

    [Display(Name = "Patient")]
    Patient = 4
}