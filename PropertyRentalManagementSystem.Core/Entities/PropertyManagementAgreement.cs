using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class PropertyManagementAgreement
{
    public int AgreementId { get; set; }

    public int OwnerId { get; set; }

    public decimal MonthlyManagementFee { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<AgreementProperty> AgreementProperties { get; set; } = new List<AgreementProperty>();

    public virtual PropertyOwner Owner { get; set; } = null!;
}
