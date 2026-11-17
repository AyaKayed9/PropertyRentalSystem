using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class MaintenanceExpense
{
    public int MaintenanceExpenseId { get; set; }

    public int MaintenanceRequestId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly ExpenseDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateOnly? PaidDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Notes { get; set; }

    public virtual MaintenanceRequest MaintenanceRequest { get; set; } = null!;
}
