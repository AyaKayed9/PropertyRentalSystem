using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class Unit
{
    public int UnitId { get; set; }

    public int PropertyId { get; set; }

    public string UnitNumber { get; set; } = null!;

    public decimal? AreaSize { get; set; }

    public byte? Bedrooms { get; set; }

    public byte? Bathrooms { get; set; }

    public decimal MonthlyRent { get; set; }

    public string UnitStatus { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    public virtual Property Property { get; set; } = null!;
}
