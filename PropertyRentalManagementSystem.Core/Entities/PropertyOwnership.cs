using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class PropertyOwnership
{
    public int PropertyId { get; set; }

    public int OwnerId { get; set; }

    public decimal OwnershipPercentage { get; set; }

    public virtual PropertyOwner Owner { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
