using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class RentPayment
{
    public int RentPaymentId { get; set; }

    public int LeaseId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Lease Lease { get; set; } = null!;

    public virtual ICollection<RentPaymentAllocation> RentPaymentAllocations { get; set; } = new List<RentPaymentAllocation>();
}
