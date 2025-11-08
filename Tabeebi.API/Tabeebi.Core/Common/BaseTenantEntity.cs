using System;

namespace Tabeebi.Core.Common;

public abstract class BaseTenantEntity : BaseEntity
{
    public Guid TenantId { get; set; }
}