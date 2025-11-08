using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

public class DoctorSchedule : BaseTenantEntity
{
    // Doctor reference
    public Guid DoctorId { get; set; }
    public virtual UserProfile Doctor { get; set; } = null!;

    // Schedule period
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsRecurring { get; set; } = false;
    public RecurrencePattern RecurrencePattern { get; set; }

    // Daily schedule
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public TimeSpan? BreakStartTime { get; set; }
    public TimeSpan? BreakEndTime { get; set; }

    // Appointment slot configuration
    public TimeSpan SlotDuration { get; set; } = TimeSpan.FromMinutes(30); // Default 30-minute slots
    public int MaxConcurrentAppointments { get; set; } = 1;
    public bool AllowOverlapping { get; set; } = false;

    // Schedule status
    public ScheduleStatus Status { get; set; } = ScheduleStatus.Active;
    public string? Notes { get; set; }

    // Exception handling
    public bool IsException { get; set; } = false; // If true, this overrides regular schedule
    public ExceptionType ExceptionType { get; set; } = ExceptionType.Available;

    // Navigation properties for time slots
    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    public virtual ICollection<ScheduleException> Exceptions { get; set; } = new List<ScheduleException>();
}

// Individual time slots that can be booked
public class TimeSlot : BaseTenantEntity
{
    public Guid DoctorScheduleId { get; set; }
    public virtual DoctorSchedule DoctorSchedule { get; set; } = null!;

    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public TimeSlotStatus Status { get; set; } = TimeSlotStatus.Available;

    // If booked, link to appointment
    public Guid? AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }

    // Booking metadata
    public DateTime? BookedAt { get; set; }
    public string? BookedBy { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? CancelledAt { get; set; }
}

// Schedule exceptions (holidays, special events, etc.)
public class ScheduleException : BaseTenantEntity
{
    public Guid DoctorScheduleId { get; set; }
    public virtual DoctorSchedule DoctorSchedule { get; set; } = null!;

    public DateTime ExceptionDate { get; set; }
    public ExceptionType ExceptionType { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Reason { get; set; }
    public bool IsRecurring { get; set; } = false;
    public RecurrencePattern? RecurrencePattern { get; set; }
    public DateTime? RecurrenceEndDate { get; set; }
}

// Enums for scheduling
public enum RecurrencePattern
{
    Daily = 0,
    Weekly = 1,
    Monthly = 2,
    Custom = 3
}

public enum ScheduleStatus
{
    Active = 0,
    Inactive = 1,
    Suspended = 2
}

public enum ExceptionType
{
    Available = 0,    // Doctor is available (overrides unavailability)
    Unavailable = 1,  // Doctor is unavailable (overrides regular schedule)
    Holiday = 2,      // Clinic holiday
    Personal = 3,     // Personal leave
    Conference = 4,   // Conference/training
    Emergency = 5     // Emergency closure
}

public enum TimeSlotStatus
{
    Available = 0,
    Booked = 1,
    Blocked = 2,      // Manually blocked
    Reserved = 3,     // Temporarily reserved
    Cancelled = 4     // Was booked but cancelled
}