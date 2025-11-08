using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

// Daily analytics snapshot
public class DailyAnalytics : BaseTenantEntity
{
    // Date context
    public DateTime Date { get; set; }
    public string Period { get; set; } = string.Empty; // "2024-01-15", "2024-W03", "2024-01"

    // Appointment metrics
    public int TotalAppointments { get; set; } = 0;
    public int CompletedAppointments { get; set; } = 0;
    public int CancelledAppointments { get; set; } = 0;
    public int NoShowAppointments { get; set; } = 0;
    public double ShowRate { get; set; } = 0; // Percentage

    // Patient metrics
    public int NewPatients { get; set; } = 0;
    public int ReturningPatients { get; set; } = 0;
    public int TotalActivePatients { get; set; } = 0;
    public int TotalPatients { get; set; } = 0;

    // Financial metrics
    public decimal TotalRevenue { get; set; } = 0;
    public decimal ConsultationRevenue { get; set; } = 0;
    public decimal ProcedureRevenue { get; set; } = 0;
    public decimal MedicationRevenue { get; set; } = 0;
    public decimal InsuranceRevenue { get; set; } = 0;
    public decimal PatientRevenue { get; set; } = 0;
    public decimal AverageRevenuePerPatient { get; set; } = 0;

    // Doctor performance
    public int ActiveDoctors { get; set; } = 0;
    public double AveragePatientsPerDoctor { get; set; } = 0;
    public double AverageRevenuePerDoctor { get; set; } = 0;
    public double AverageAppointmentsPerDoctor { get; set; } = 0;

    // Service analytics
    public int TotalServicesProvided { get; set; } = 0;
    public string? MostPopularService { get; set; }
    public int MostPopularServiceCount { get; set; } = 0;

    // Time metrics
    public double AverageAppointmentDuration { get; set; } = 0; // In minutes
    public double AverageWaitTime { get; set; } = 0; // In minutes
    public TimeSpan TotalClinicHours { get; set; } = TimeSpan.Zero;
    public double UtilizationRate { get; set; } = 0; // Percentage

    // Navigation properties
    public virtual ICollection<DoctorAnalytics> DoctorAnalytics { get; set; } = new List<DoctorAnalytics>();
    public virtual ICollection<ServiceAnalytics> ServiceAnalytics { get; set; } = new List<ServiceAnalytics>();
}

// Doctor-specific analytics
public class DoctorAnalytics : BaseTenantEntity
{
    public Guid DailyAnalyticsId { get; set; }
    public virtual DailyAnalytics DailyAnalytics { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public virtual UserProfile Doctor { get; set; } = null!;

    public DateTime Date { get; set; }

    // Appointment metrics for doctor
    public int TotalAppointments { get; set; } = 0;
    public int CompletedAppointments { get; set; } = 0;
    public int CancelledAppointments { get; set; } = 0;
    public int NoShowAppointments { get; set; } = 0;
    public double ShowRate { get; set; } = 0;

    // Patient metrics for doctor
    public int NewPatients { get; set; } = 0;
    public int ReturningPatients { get; set; } = 0;
    public int UniquePatients { get; set; } = 0;

    // Financial metrics for doctor
    public decimal TotalRevenue { get; set; } = 0;
    public decimal AverageRevenuePerPatient { get; set; } = 0;
    public int TotalServicesProvided { get; set; } = 0;

    // Time metrics for doctor
    public TimeSpan WorkingHours { get; set; } = TimeSpan.Zero;
    public TimeSpan ScheduledTime { get; set; } = TimeSpan.Zero;
    public TimeSpan PatientTime { get; set; } = TimeSpan.Zero;
    public double UtilizationRate { get; set; } = 0;
    public double AverageAppointmentDuration { get; set; } = 0;
}

// Service-specific analytics
public class ServiceAnalytics : BaseTenantEntity
{
    public Guid DailyAnalyticsId { get; set; }
    public virtual DailyAnalytics DailyAnalytics { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;

    public DateTime Date { get; set; }

    // Service metrics
    public int TimesProvided { get; set; } = 0;
    public decimal TotalRevenue { get; set; } = 0;
    public decimal AverageRevenue { get; set; } = 0;
    public int UniquePatients { get; set; } = 0;
    public TimeSpan TotalDuration { get; set; } = TimeSpan.Zero;
    public double AverageDuration { get; set; } = 0;

    // Popularity metrics
    public int TimesBooked { get; set; } = 0;
    public int TimesCancelled { get; set; } = 0;
    public double CancellationRate { get; set; } = 0;
}

// Patient lifecycle analytics
public class PatientAnalytics : BaseTenantEntity
{
    public Guid PatientId { get; set; }
    public virtual UserProfile Patient { get; set; } = null!;

    public DateTime Date { get; set; }
    public AnalyticsPeriod Period { get; set; }

    // Visit history
    public int TotalVisits { get; set; } = 0;
    public int VisitsInPeriod { get; set; } = 0;
    public DateTime? LastVisitDate { get; set; }
    public DateTime? FirstVisitDate { get; set; }

    // Financial history
    public decimal TotalSpent { get; set; } = 0;
    public decimal SpentInPeriod { get; set; } = 0;
    public decimal AverageSpentPerVisit { get; set; } = 0;

    // Appointment behavior
    public int TotalAppointments { get; set; } = 0;
    public int CancelledAppointments { get; set; } = 0;
    public int NoShowAppointments { get; set; } = 0;
    public double ShowRate { get; set; } = 0;
    public double CancellationRate { get; set; } = 0;

    // Service preferences
    public string? MostUsedService { get; set; }
    public string? PreferredDoctor { get; set; }
    public Guid? PreferredDoctorId { get; set; }

    // Status
    public PatientStatus Status { get; set; } = PatientStatus.Active;
    public bool IsHighRisk { get; set; } = false;
    public bool IsChronic { get; set; } = false;
}

// Financial analytics
public class FinancialAnalytics : BaseTenantEntity
{
    public DateTime Date { get; set; }
    public AnalyticsPeriod Period { get; set; }

    // Revenue breakdown
    public decimal TotalRevenue { get; set; } = 0;
    public decimal ConsultationRevenue { get; set; } = 0;
    public decimal ProcedureRevenue { get; set; } = 0;
    public decimal MedicationRevenue { get; set; } = 0;
    public decimal OtherRevenue { get; set; } = 0;

    // Payment method breakdown
    public decimal CashRevenue { get; set; } = 0;
    public decimal CardRevenue { get; set; } = 0;
    public decimal InsuranceRevenue { get; set; } = 0;
    public decimal OnlineRevenue { get; set; } = 0;

    // Insurance metrics
    public decimal TotalClaims { get; set; } = 0;
    public decimal ApprovedClaims { get; set; } = 0;
    public decimal PendingClaims { get; set; } = 0;
    public decimal DeniedClaims { get; set; } = 0;
    public double ClaimApprovalRate { get; set; } = 0;
    public decimal AverageClaimAmount { get; set; } = 0;

    // Financial health
    public decimal OutstandingInvoices { get; set; } = 0;
    public decimal OverdueAmount { get; set; } = 0;
    public double CollectionRate { get; set; } = 0;
    public decimal AverageInvoiceAmount { get; set; } = 0;
    public int AverageDaysToPay { get; set; } = 0;
}

// System usage analytics
public class SystemAnalytics : BaseTenantEntity
{
    public DateTime Date { get; set; }
    public AnalyticsPeriod Period { get; set; }

    // User activity
    public int ActiveUsers { get; set; } = 0;
    public int TotalLogins { get; set; } = 0;
    public int UniqueLogins { get; set; } = 0;
    public double AverageSessionDuration { get; set; } = 0; // In minutes

    // Feature usage
    public int AppointmentsBookedOnline { get; set; } = 0;
    public int AppointmentsBookedByStaff { get; set; } = 0;
    public int OnlinePayments { get; set; } = 0;
    public int PrescriptionsGenerated { get; set; } = 0;
    public int MedicalRecordsCreated { get; set; } = 0;

    // System performance
    public double AverageResponseTime { get; set; } = 0; // In milliseconds
    public int ErrorCount { get; set; } = 0;
    public int TotalRequests { get; set; } = 0;
    public double UptimePercentage { get; set; } = 0;

    // Storage and data
    public long StorageUsed { get; set; } = 0; // In bytes
    public int TotalRecords { get; set; } = 0;
    public int TotalAttachments { get; set; } = 0;
}

// Enums for analytics
public enum AnalyticsPeriod
{
    Daily = 0,
    Weekly = 1,
    Monthly = 2,
    Quarterly = 3,
    Yearly = 4
}

public enum PatientStatus
{
    Active = 0,
    Inactive = 1,
    Churned = 2,
    New = 3,
    Returning = 4
}