using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class PropertyOwner
{
    public int OwnerId { get; set; }

    public string OwnerCategory { get; set; } = null!;

    public string? EntityType { get; set; }

    public string DisplayName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OwnerSettlement> OwnerSettlements { get; set; } = new List<OwnerSettlement>();

    public virtual ICollection<PropertyManagementAgreement> PropertyManagementAgreements { get; set; } = new List<PropertyManagementAgreement>();

    public virtual ICollection<PropertyOwnership> PropertyOwnerships { get; set; } = new List<PropertyOwnership>();
}
