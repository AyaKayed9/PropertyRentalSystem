using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class OwnerSettlementPayment
{
    public int SettlementPaymentId { get; set; }

    public int SettlementId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual OwnerSettlement Settlement { get; set; } = null!;
}
