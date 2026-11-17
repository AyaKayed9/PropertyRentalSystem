using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class Property
{
    public int PropertyId { get; set; }

    public int PropertyTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string PropertyStatus { get; set; } = null!;

    public string City { get; set; } = null!;

    public string StreetAddress { get; set; } = null!;

    public decimal AreaSize { get; set; }

    public short? YearBuilt { get; set; }

    public virtual ICollection<AgreementProperty> AgreementProperties { get; set; } = new List<AgreementProperty>();

    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    public virtual ICollection<OwnerSettlementItem> OwnerSettlementItems { get; set; } = new List<OwnerSettlementItem>();

    public virtual ICollection<PropertyOwnership> PropertyOwnerships { get; set; } = new List<PropertyOwnership>();

    public virtual PropertyType PropertyType { get; set; } = null!;

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
