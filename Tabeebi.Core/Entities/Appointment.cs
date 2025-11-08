using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

public enum AppointmentStatus
{
    Scheduled = 0,
    Confirmed = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4,
    NoShow = 5,
    Rescheduled = 6,
    PendingConfirmation = 7,
    Overdue = 8
}

public enum AppointmentType
{
    Regular = 0,
    FollowUp = 1,
    Emergency = 2,
    Consultation = 3,
    Procedure = 4,
    Surgery = 5,
    Telemedicine = 6,
    WalkIn = 7
}

public class Appointment : BaseTenantEntity
{
    // Basic appointment information
    public DateTime ScheduledDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public AppointmentType Type { get; set; } = AppointmentType.Regular;

    // Patient and Doctor information - updated to use UserProfile
    public Guid PatientId { get; set; }
    public virtual UserProfile Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public virtual UserProfile Doctor { get; set; } = null!;

    // Slot management for conflict prevention
    public Guid? TimeSlotId { get; set; }
    public virtual TimeSlot? TimeSlot { get; set; }
    public bool IsSlotReserved { get; set; } = false;
    public DateTime? SlotReservedUntil { get; set; }
    public string? ReservationToken { get; set; }

    // Room and resource management
    public string? RoomNumber { get; set; }
    public string? RoomName { get; set; }
    public bool RequiresRoom { get; set; } = true;

    // Appointment details
    public string? Notes { get; set; }
    public string? ReasonForVisit { get; set; }
    public string? PatientNotes { get; set; } // Notes provided by patient during booking
    public int Priority { get; set; } = 0; // 0 = normal, higher numbers = higher priority

    // Booking metadata
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public string? BookedBy { get; set; }
    public string? BookingChannel { get; set; } // e.g., "Website", "Mobile App", "Phone"

    // Confirmation and reminders
    public bool ConfirmationRequired { get; set; } = true;
    public DateTime? ConfirmationDate { get; set; }
    public string? ConfirmationMethod { get; set; } // e.g., "SMS", "Email", "Phone"
    public bool ReminderSent { get; set; } = false;
    public DateTime? ReminderSentDate { get; set; }

    // Check-in/Check-out process
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? CheckedInBy { get; set; }
    public string? CheckedOutBy { get; set; }
    public TimeSpan? WaitTime { get; set; } // Calculated: CheckInTime - ScheduledDateTime
    public TimeSpan? ConsultationDuration { get; set; } // Calculated: CheckOutTime - CheckInTime

    // Cancellation management
    public DateTime? CancelledDate { get; set; }
    public string? CancellationReason { get; set; }
    public string? CancelledBy { get; set; }
    public bool CancellationFeeWaived { get; set; } = false;

    // Rescheduling tracking
    public DateTime? RescheduledFrom { get; set; }
    public DateTime? RescheduledTo { get; set; }
    public int RescheduleCount { get; set; } = 0;
    public string? RescheduledBy { get; set; }
    public string? RescheduleReason { get; set; }

    // Payment information (simplified)
    public decimal Cost { get; set; }
    public bool IsPaid { get; set; } = false;
    public DateTime? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }

    // Follow-up management
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpNotes { get; set; }
    public bool FollowUpScheduled { get; set; } = false;
    public Guid? FollowUpAppointmentId { get; set; }
    public virtual Appointment? FollowUpAppointment { get; set; }

    // Clinical workflow integration
    public Guid? MedicalRecordId { get; set; }
    public virtual MedicalRecord? MedicalRecord { get; set; }
    public bool MedicalRecordCreated { get; set; } = false;

    // Navigation properties
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}