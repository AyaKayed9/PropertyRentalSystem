using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class OwnerSettlementItem
{
    public int SettlementItemId { get; set; }

    public int SettlementId { get; set; }

    public int? PropertyId { get; set; }

    public string ItemType { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public virtual Property? Property { get; set; }

    public virtual OwnerSettlement Settlement { get; set; } = null!;
}
