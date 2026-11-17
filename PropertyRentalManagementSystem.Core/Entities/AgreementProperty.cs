using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class AgreementProperty
{
    public int AgreementPropertyId { get; set; }

    public int AgreementId { get; set; }

    public int PropertyId { get; set; }

    public decimal CommissionRate { get; set; }

    public virtual PropertyManagementAgreement Agreement { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
