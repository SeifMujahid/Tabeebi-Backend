using Microsoft.EntityFrameworkCore;

namespace Tabeebi.Infrastructure;

/// <summary>
/// This is a placeholder file to establish the Infrastructure layer structure.
/// In the future, this layer will contain:
/// - EF Core DbContext
/// - Repository implementations
/// - Database configurations
/// - External service integrations
/// </summary>
public static class InfrastructureLayerInfo
{
    public const string LayerName = "Infrastructure";
    public const string Responsibility = "Data Access, Repositories, External Services";

    // Future: DbContext will be implemented here
    // public class TabeebiDbContext : DbContext
    // {
    //     // DbSet properties for entities will go here
    // }
}