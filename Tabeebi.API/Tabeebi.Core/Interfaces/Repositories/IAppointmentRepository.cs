using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for Appointment entity
/// </summary>
public interface IAppointmentRepository : ITenantRepository<Appointment>
{
    /// <summary>
    /// Get appointments by doctor within date range
    /// </summary>
    /// <param name="doctorId">Doctor ID</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of appointments</returns>
    Task<IReadOnlyList<Appointment>> GetByDoctorAsync(Guid doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointments by patient
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <param name="includePast">Include past appointments</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of patient appointments</returns>
    Task<IReadOnlyList<Appointment>> GetByPatientAsync(Guid patientId, bool includePast = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointments by status
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="status">Appointment status</param>
    /// <param name="startDate">Optional start date filter</param>
    /// <param name="endDate">Optional end date filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of appointments with specified status</returns>
    Task<IReadOnlyList<Appointment>> GetByStatusAsync(Guid tenantId, AppointmentStatus status, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointments for a specific date
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="date">Date to get appointments for</param>
    /// <param name="doctorId">Optional doctor filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of appointments for the specified date</returns>
    Task<IReadOnlyList<Appointment>> GetByDateAsync(Guid tenantId, DateTime date, Guid? doctorId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get today's appointments
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="doctorId">Optional doctor filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of today's appointments</returns>
    Task<IReadOnlyList<Appointment>> GetTodayAppointmentsAsync(Guid tenantId, Guid? doctorId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get upcoming appointments
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="doctorId">Optional doctor filter</param>
    /// <param name="daysAhead">Number of days ahead to look</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of upcoming appointments</returns>
    Task<IReadOnlyList<Appointment>> GetUpcomingAppointmentsAsync(Guid tenantId, Guid? doctorId = null, int daysAhead = 7, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if time slot is available
    /// </summary>
    /// <param name="doctorId">Doctor ID</param>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="excludeAppointmentId">Appointment ID to exclude (for updates)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if time slot is available</returns>
    Task<bool> IsTimeSlotAvailableAsync(Guid doctorId, DateTime startTime, DateTime endTime, Guid? excludeAppointmentId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointment conflicts
    /// </summary>
    /// <param name="doctorId">Doctor ID</param>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="excludeAppointmentId">Appointment ID to exclude</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of conflicting appointments</returns>
    Task<IReadOnlyList<Appointment>> GetConflictingAppointmentsAsync(Guid doctorId, DateTime startTime, DateTime endTime, Guid? excludeAppointmentId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointments with related entities
    /// </summary>
    /// <param name="id">Appointment ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Appointment with related entities loaded</returns>
    Task<Appointment?> GetWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get unpaid appointments
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="patientId">Optional patient filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of unpaid appointments</returns>
    Task<IReadOnlyList<Appointment>> GetUnpaidAppointmentsAsync(Guid tenantId, Guid? patientId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointments requiring follow-up
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="doctorId">Optional doctor filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of appointments requiring follow-up</returns>
    Task<IReadOnlyList<Appointment>> GetAppointmentsRequiringFollowUpAsync(Guid tenantId, Guid? doctorId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get cancelled appointments
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="startDate">Optional start date</param>
    /// <param name="endDate">Optional end date</param>
    /// <param name="reason">Optional cancellation reason filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of cancelled appointments</returns>
    Task<IReadOnlyList<Appointment>> GetCancelledAppointmentsAsync(Guid tenantId, DateTime? startDate = null, DateTime? endDate = null, string? reason = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get no-show appointments
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="startDate">Optional start date</param>
    /// <param name="endDate">Optional end date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of no-show appointments</returns>
    Task<IReadOnlyList<Appointment>> GetNoShowAppointmentsAsync(Guid tenantId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointments by room
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="roomNumber">Room number</param>
    /// <param name="date">Optional date filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of appointments in specified room</returns>
    Task<IReadOnlyList<Appointment>> GetByRoomAsync(Guid tenantId, string roomNumber, DateTime? date = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search appointments by reason for visit
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="searchTerm">Search term</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching appointments</returns>
    Task<IReadOnlyList<Appointment>> SearchByReasonAsync(Guid tenantId, string searchTerm, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get appointment statistics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Appointment statistics</returns>
    Task<AppointmentStats> GetAppointmentStatsAsync(Guid tenantId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}

/// <summary>
/// Appointment statistics
/// </summary>
public class AppointmentStats
{
    public int TotalAppointments { get; set; }
    public int CompletedAppointments { get; set; }
    public int CancelledAppointments { get; set; }
    public int NoShowAppointments { get; set; }
    public double ShowRate { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageRevenue { get; set; }
    public TimeSpan AverageWaitTime { get; set; }
    public TimeSpan AverageConsultationTime { get; set; }
    public Dictionary<AppointmentStatus, int> StatusBreakdown { get; set; } = new Dictionary<AppointmentStatus, int>();
    public Dictionary<AppointmentType, int> TypeBreakdown { get; set; } = new Dictionary<AppointmentType, int>();
}