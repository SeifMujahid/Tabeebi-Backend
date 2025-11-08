using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;

namespace Tabeebi.Core.Entities;

/// <summary>
/// Core user entity that will be mapped to ASP.NET Core Identity tables in the Infrastructure layer
/// This entity contains only the essential authentication properties without framework dependencies
/// </summary>
public class IdentityUser : BaseTenantEntity
{
    /// <summary>
    /// User's email address (used for login)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Whether the email is confirmed
    /// </summary>
    public bool EmailConfirmed { get; set; } = false;

    /// <summary>
    /// Hashed password
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Whether the phone number is confirmed
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; } = false;

    /// <summary>
    /// Whether two-factor authentication is enabled
    /// </summary>
    public bool TwoFactorEnabled { get; set; } = false;

    /// <summary>
    /// Date and time when account was locked out (if any)
    /// </summary>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    /// Whether the account can be locked out
    /// </summary>
    public bool LockoutEnabled { get; set; } = true;

    /// <summary>
    /// Number of failed access attempts
    /// </summary>
    public int AccessFailedCount { get; set; } = 0;

    /// <summary>
    /// Security stamp for invalidating sessions
    /// </summary>
    public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Concurrency stamp for optimistic concurrency
    /// </summary>
    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Whether the account is enabled
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date when user was last active
    /// </summary>
    public DateTime? LastActiveDate { get; set; }

    /// <summary>
    /// User's preferred language
    /// </summary>
    public string? PreferredLanguage { get; set; }

    /// <summary>
    /// User's time zone
    /// </summary>
    public string? TimeZone { get; set; }

    /// <summary>
    /// Date when password was last changed
    /// </summary>
    public DateTime? PasswordChangedDate { get; set; }

    // Navigation properties
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual UserProfile UserProfile { get; set; } = null!;

    // These will be mapped to ASP.NET Core Identity tables in Infrastructure layer
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
    public virtual ICollection<UserLogin> Logins { get; set; } = new List<UserLogin>();
    public virtual ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
}

/// <summary>
/// Represents a user claim (to be mapped to IdentityUserClaim in Infrastructure)
/// </summary>
public class UserClaim : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}

/// <summary>
/// Represents a user login (to be mapped to IdentityUserLogin in Infrastructure)
/// </summary>
public class UserLogin : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public string LoginProvider { get; set; } = string.Empty;
    public string ProviderKey { get; set; } = string.Empty;
    public string? ProviderDisplayName { get; set; }
}

/// <summary>
/// Represents a user token (to be mapped to IdentityUserToken in Infrastructure)
/// </summary>
public class UserToken : BaseTenantEntity
{
    public Guid IdentityUserId { get; set; }
    public virtual IdentityUser IdentityUser { get; set; } = null!;

    public string LoginProvider { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Value { get; set; }
}