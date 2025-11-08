using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for Tenant entity
/// </summary>
public interface ITenantRepository : IRepository<Tenant>
{
    /// <summary>
    /// Get tenant by subdomain
    /// </summary>
    /// <param name="subdomain">Tenant subdomain</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Tenant if found, null otherwise</returns>
    Task<Tenant?> GetBySubdomainAsync(string subdomain, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get active tenants
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of active tenants</returns>
    Task<IReadOnlyList<Tenant>> GetActiveTenantsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tenants by subscription plan
    /// </summary>
    /// <param name="subscriptionPlan">Subscription plan</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of tenants with specified plan</returns>
    Task<IReadOnlyList<Tenant>> GetBySubscriptionPlanAsync(string subscriptionPlan, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tenants with expiring trials
    /// </summary>
    /// <param name="days">Days until expiry</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of tenants with expiring trials</returns>
    Task<IReadOnlyList<Tenant>> GetTenantsWithExpiringTrialsAsync(int days, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if subdomain is available
    /// </summary>
    /// <param name="subdomain">Subdomain to check</param>
    /// <param name="excludeId">Tenant ID to exclude from check (for updates)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if subdomain is available</returns>
    Task<bool> IsSubdomainAvailableAsync(string subdomain, Guid? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update tenant status
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="status">New status</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateTenantStatusAsync(Guid tenantId, TenantStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tenant usage statistics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Tenant usage statistics</returns>
    Task<TenantUsageStats?> GetUsageStatsAsync(Guid tenantId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Tenant usage statistics
/// </summary>
public class TenantUsageStats
{
    public int ActiveUsers { get; set; }
    public int TotalAppointments { get; set; }
    public int TotalPatients { get; set; }
    public int TotalDoctors { get; set; }
    public decimal TotalRevenue { get; set; }
    public long StorageUsedBytes { get; set; }
    public DateTime LastActivityDate { get; set; }
}