using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities;

// Permission definition
public class Permission : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty; // e.g., "Appointments", "Patients", "Billing"
    public string Action { get; set; } = string.Empty; // e.g., "Create", "Read", "Update", "Delete"
    public string Resource { get; set; } = string.Empty; // e.g., "Appointment", "MedicalRecord"
    public bool IsSystem { get; set; } = false; // System permissions cannot be modified
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}

// Role definition (extends ASP.NET Core Identity roles)
public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsSystem { get; set; } = false; // System roles cannot be modified
    public bool IsActive { get; set; } = true;
    public int Priority { get; set; } = 0; // Higher number = higher priority

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

// Role-Permission mapping
public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    public Guid PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;

    public bool IsGranted { get; set; } = true; // Can be used to explicitly deny
    public string? GrantedBy { get; set; }
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}

// User-Permission mapping (for individual user permissions)
public class UserPermission : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public Guid PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;

    public bool IsGranted { get; set; } = true;
    public string? GrantedBy { get; set; }
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
    public string? Notes { get; set; }
}

// User-Role mapping
public class UserRole : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    public string? AssignedBy { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
}

// Permission scopes for more granular control
public class PermissionScope : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ScopeType ScopeType { get; set; }
    public string? ScopeValue { get; set; } // e.g., tenant ID, department ID, etc.

    // Navigation properties
    public virtual ICollection<UserPermissionScope> UserPermissionScopes { get; set; } = new List<UserPermissionScope>();
}

// User-Permission-Scope mapping
public class UserPermissionScope : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public Guid PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;

    public Guid ScopeId { get; set; }
    public virtual PermissionScope Scope { get; set; } = null!;

    public bool IsGranted { get; set; } = true;
    public string? GrantedBy { get; set; }
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
}

// Audit log for permission changes
public class PermissionAuditLog : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public Guid? RoleId { get; set; }
    public virtual Role? Role { get; set; }

    public Guid PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;

    public Guid? ScopeId { get; set; }
    public virtual PermissionScope? Scope { get; set; }

    public PermissionAction Action { get; set; }
    public bool IsGranted { get; set; }
    public string? Reason { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    public string? IPAddress { get; set; }
    public string? UserAgent { get; set; }
}

// Enums for permission system
public enum ScopeType
{
    All = 0,          // All resources
    Tenant = 1,       // Tenant-scoped resources
    Department = 2,   // Department-scoped resources
    Personal = 3,     // User's own resources only
    Custom = 99       // Custom scope defined by ScopeValue
}

public enum PermissionAction
{
    Granted = 0,
    Revoked = 1,
    Modified = 2,
    Expired = 3
}

// Predefined system permissions (static class for constants)
public static class SystemPermissions
{
    // Appointment permissions
    public const string APPOINTMENT_CREATE = "appointments.create";
    public const string APPOINTMENT_READ = "appointments.read";
    public const string APPOINTMENT_READ_OWN = "appointments.read.own";
    public const string APPOINTMENT_UPDATE = "appointments.update";
    public const string APPOINTMENT_UPDATE_OWN = "appointments.update.own";
    public const string APPOINTMENT_DELETE = "appointments.delete";
    public const string APPOINTMENT_CANCEL = "appointments.cancel";
    public const string APPOINTMENT_CHECKIN = "appointments.checkin";
    public const string APPOINTMENT_MANAGE_SCHEDULE = "appointments.manage_schedule";

    // Patient permissions
    public const string PATIENT_CREATE = "patients.create";
    public const string PATIENT_READ = "patients.read";
    public const string PATIENT_READ_OWN = "patients.read.own";
    public const string PATIENT_UPDATE = "patients.update";
    public const string PATIENT_UPDATE_OWN = "patients.update.own";
    public const string PATIENT_DELETE = "patients.delete";
    public const string PATIENT_MEDICAL_HISTORY = "patients.medical_history";

    // Medical record permissions
    public const string MEDICAL_RECORD_CREATE = "medical_records.create";
    public const string MEDICAL_RECORD_READ = "medical_records.read";
    public const string MEDICAL_RECORD_READ_OWN = "medical_records.read.own";
    public const string MEDICAL_RECORD_UPDATE = "medical_records.update";
    public const string MEDICAL_RECORD_DELETE = "medical_records.delete";
    public const string PRESCRIPTION_CREATE = "prescriptions.create";
    public const string PRESCRIPTION_READ = "prescriptions.read";
    public const string PRESCRIPTION_UPDATE = "prescriptions.update";
    public const string PRESCRIPTION_DELETE = "prescriptions.delete";

    // Billing permissions
    public const string PAYMENT_CREATE = "payments.create";
    public const string PAYMENT_READ = "payments.read";
    public const string PAYMENT_UPDATE = "payments.update";
    public const string PAYMENT_DELETE = "payments.delete";
    public const string INVOICE_CREATE = "invoices.create";
    public const string INVOICE_READ = "invoices.read";
    public const string INVOICE_UPDATE = "invoices.update";
    public const string INVOICE_DELETE = "invoices.delete";

    // Analytics permissions
    public const string ANALYTICS_READ = "analytics.read";
    public const string ANALYTICS_FINANCIAL = "analytics.financial";
    public const string ANALYTICS_CLINICAL = "analytics.clinical";
    public const string REPORTS_GENERATE = "reports.generate";

    // User management permissions
    public const string USER_CREATE = "users.create";
    public const string USER_READ = "users.read";
    public const string USER_UPDATE = "users.update";
    public const string USER_DELETE = "users.delete";
    public const string USER_MANAGE_ROLES = "users.manage_roles";
    public const string USER_MANAGE_PERMISSIONS = "users.manage_permissions";

    // System permissions
    public const string SYSTEM_SETTINGS = "system.settings";
    public const string SYSTEM_BACKUP = "system.backup";
    public const string SYSTEM_AUDIT = "system.audit";
    public const string TENANT_MANAGE = "tenant.manage";
    public const string CLINIC_SETTINGS = "clinic.settings";
}