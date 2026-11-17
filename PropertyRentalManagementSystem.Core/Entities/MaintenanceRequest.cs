using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class MaintenanceRequest
{
    public int MaintenanceRequestId { get; set; }

    public int PropertyId { get; set; }

    public int? UnitId { get; set; }

    public int? ReportedByTenantId { get; set; }

    public int RecordedByUserId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly ReportedDate { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly? ResolutionDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<MaintenanceExpense> MaintenanceExpenses { get; set; } = new List<MaintenanceExpense>();

    public virtual Property Property { get; set; } = null!;

    public virtual User RecordedByUser { get; set; } = null!;

    public virtual Tenant? ReportedByTenant { get; set; }

    public virtual Unit? Unit { get; set; }
}
