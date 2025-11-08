using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for UserProfile entity
/// </summary>
public interface IUserProfileRepository : ITenantRepository<UserProfile>
{
    /// <summary>
    /// Get user profile by Identity User ID
    /// </summary>
    /// <param name="identityUserId">Identity User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User profile if found, null otherwise</returns>
    Task<UserProfile?> GetByIdentityUserIdAsync(Guid identityUserId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user profiles by profile type
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="profileType">Profile type</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of user profiles with specified type</returns>
    Task<IReadOnlyList<UserProfile>> GetByProfileTypeAsync(Guid tenantId, UserProfileType profileType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get doctors (user profiles with Doctor profile type)
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="specialization">Optional specialization filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of doctors</returns>
    Task<IReadOnlyList<UserProfile>> GetDoctorsAsync(Guid tenantId, string? specialization = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get patients (user profiles with Patient profile type)
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="searchTerm">Optional search term for name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of patients</returns>
    Task<IReadOnlyList<UserProfile>> GetPatientsAsync(Guid tenantId, string? searchTerm = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get staff (user profiles with Receptionist profile type)
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of staff members</returns>
    Task<IReadOnlyList<UserProfile>> GetStaffAsync(Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search user profiles by name
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="searchTerm">Search term</param>
    /// <param name="profileType">Optional profile type filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching user profiles</returns>
    Task<IReadOnlyList<UserProfile>> SearchByNameAsync(Guid tenantId, string searchTerm, UserProfileType? profileType = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user profile by email (through IdentityUser)
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="email">Email address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User profile if found, null otherwise</returns>
    Task<UserProfile?> GetByEmailAsync(Guid tenantId, string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user profile by phone number
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="phoneNumber">Phone number</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User profile if found, null otherwise</returns>
    Task<UserProfile?> GetByPhoneNumberAsync(Guid tenantId, string phoneNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user profile by national ID
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="nationalId">National ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User profile if found, null otherwise</returns>
    Task<UserProfile?> GetByNationalIdAsync(Guid tenantId, string nationalId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if user profile exists for Identity User
    /// </summary>
    /// <param name="identityUserId">Identity User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if profile exists</returns>
    Task<bool> ExistsByIdentityUserIdAsync(Guid identityUserId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get doctors available for appointments on specific date
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="date">Date for availability check</param>
    /// <param name="specialization">Optional specialization filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of available doctors</returns>
    Task<IReadOnlyList<UserProfile>> GetAvailableDoctorsAsync(Guid tenantId, DateTime date, string? specialization = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get active user profiles (not deleted and active)
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="profileType">Optional profile type filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of active user profiles</returns>
    Task<IReadOnlyList<UserProfile>> GetActiveProfilesAsync(Guid tenantId, UserProfileType? profileType = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user profile statistics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User profile statistics</returns>
    Task<UserProfileStats> GetProfileStatsAsync(Guid tenantId, CancellationToken cancellationToken = default);
}

/// <summary>
/// User profile statistics
/// </summary>
public class UserProfileStats
{
    public int TotalUsers { get; set; }
    public int ActiveUsers { get; set; }
    public int Doctors { get; set; }
    public int Patients { get; set; }
    public int Receptionists { get; set; }
    public int ClinicOwners { get; set; }
    public int SuperAdmins { get; set; }
    public int NewUsersThisMonth { get; set; }
    public int ActiveUsersThisMonth { get; set; }
}