using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class OwnerSettlement
{
    public int SettlementId { get; set; }

    public int OwnerId { get; set; }

    public DateOnly SettlementPeriod { get; set; }

    public DateOnly SettlementDate { get; set; }

    public decimal PreviousBalance { get; set; }

    public decimal TotalRentCollected { get; set; }

    public decimal TotalManagementFees { get; set; }

    public decimal TotalCommissions { get; set; }

    public decimal TotalMaintenanceExpenses { get; set; }

    public decimal NetAmount { get; set; }

    public virtual PropertyOwner Owner { get; set; } = null!;

    public virtual ICollection<OwnerSettlementItem> OwnerSettlementItems { get; set; } = new List<OwnerSettlementItem>();

    public virtual ICollection<OwnerSettlementPayment> OwnerSettlementPayments { get; set; } = new List<OwnerSettlementPayment>();
}
