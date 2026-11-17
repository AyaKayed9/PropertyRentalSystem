using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class RentPaymentAllocation
{
    public int AllocationId { get; set; }

    public int RentPaymentId { get; set; }

    public int ChargeId { get; set; }

    public decimal AllocatedAmount { get; set; }

    public virtual RentCharge Charge { get; set; } = null!;

    public virtual RentPayment RentPayment { get; set; } = null!;
}
