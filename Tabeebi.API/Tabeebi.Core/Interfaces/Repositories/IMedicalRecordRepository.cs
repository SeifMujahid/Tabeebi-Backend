using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for MedicalRecord entity
/// </summary>
public interface IMedicalRecordRepository : ITenantRepository<MedicalRecord>
{
    /// <summary>
    /// Get medical records by patient
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <param name="includeDeleted">Include deleted records</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of patient medical records</returns>
    Task<IReadOnlyList<MedicalRecord>> GetByPatientAsync(Guid patientId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records by doctor
    /// </summary>
    /// <param name="doctorId">Doctor ID</param>
    /// <param name="startDate">Optional start date filter</param>
    /// <param name="endDate">Optional end date filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of doctor's medical records</returns>
    Task<IReadOnlyList<MedicalRecord>> GetByDoctorAsync(Guid doctorId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records by status
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="status">Medical record status</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of medical records with specified status</returns>
    Task<IReadOnlyList<MedicalRecord>> GetByStatusAsync(Guid tenantId, MedicalRecordStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records by appointment
    /// </summary>
    /// <param name="appointmentId">Appointment ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Medical record if found, null otherwise</returns>
    Task<MedicalRecord?> GetByAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records with diagnoses
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <param name="diagnosisCode">Optional diagnosis code filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of medical records with specified diagnosis</returns>
    Task<IReadOnlyList<MedicalRecord>> GetByDiagnosisAsync(Guid patientId, string? diagnosisCode = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records by date range
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="doctorId">Optional doctor filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of medical records within date range</returns>
    Task<IReadOnlyList<MedicalRecord>> GetByDateRangeAsync(Guid tenantId, DateTime startDate, DateTime endDate, Guid? doctorId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get unsigned medical records
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="doctorId">Optional doctor filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of unsigned medical records</returns>
    Task<IReadOnlyList<MedicalRecord>> GetUnsignedRecordsAsync(Guid tenantId, Guid? doctorId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records with attachments
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of medical records with attachments loaded</returns>
    Task<IReadOnlyList<MedicalRecord>> GetWithAttachmentsAsync(Guid patientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search medical records by chief complaint
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="searchTerm">Search term</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching medical records</returns>
    Task<IReadOnlyList<MedicalRecord>> SearchByChiefComplaintAsync(Guid tenantId, string searchTerm, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records requiring follow-up
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="followUpDate">Follow-up date filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of medical records requiring follow-up</returns>
    Task<IReadOnlyList<MedicalRecord>> GetRequiringFollowUpAsync(Guid tenantId, DateTime? followUpDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get corrected medical records
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of corrected medical records</returns>
    Task<IReadOnlyList<MedicalRecord>> GetCorrectedRecordsAsync(Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical records with prescriptions
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <param name="medicationName">Optional medication name filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of medical records with specified medication</returns>
    Task<IReadOnlyList<MedicalRecord>> GetByMedicationAsync(Guid patientId, string? medicationName = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get medical record statistics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Medical record statistics</returns>
    Task<MedicalRecordStats> GetMedicalRecordStatsAsync(Guid tenantId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get patient medical history summary
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Patient medical history summary</returns>
    Task<PatientMedicalHistory> GetPatientHistorySummaryAsync(Guid patientId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Medical record statistics
/// </summary>
public class MedicalRecordStats
{
    public int TotalRecords { get; set; }
    public int SignedRecords { get; set; }
    public int UnsignedRecords { get; set; }
    public int CorrectedRecords { get; set; }
    public double SigningRate { get; set; }
    public Dictionary<MedicalRecordStatus, int> StatusBreakdown { get; set; } = new Dictionary<MedicalRecordStatus, int>();
    public int UniquePatients { get; set; }
    public int UniqueDoctors { get; set; }
    public int RecordsWithAttachments { get; set; }
    public int RecordsWithPrescriptions { get; set; }
}

/// <summary>
/// Patient medical history summary
/// </summary>
public class PatientMedicalHistory
{
    public int TotalVisits { get; set; }
    public DateTime FirstVisitDate { get; set; }
    public DateTime LastVisitDate { get; set; }
    public List<string> ChronicDiagnoses { get; set; } = new List<string>();
    public List<string> CurrentMedications { get; set; } = new List<string>();
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> FamilyHistory { get; set; } = new List<string>();
    public List<string> PastSurgeries { get; set; } = new List<string>();
    public List<string> Vaccinations { get; set; } = new List<string>();
    public Dictionary<string, int> VisitFrequency { get; set; } = new Dictionary<string, int>();
}