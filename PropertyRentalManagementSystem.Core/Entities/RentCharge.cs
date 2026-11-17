using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class RentCharge
{
    public int ChargeId { get; set; }

    public int LeaseId { get; set; }

    public DateOnly ChargeMonth { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal AmountDue { get; set; }

    public string? Notes { get; set; }

    public virtual Lease Lease { get; set; } = null!;

    public virtual ICollection<RentPaymentAllocation> RentPaymentAllocations { get; set; } = new List<RentPaymentAllocation>();
}
