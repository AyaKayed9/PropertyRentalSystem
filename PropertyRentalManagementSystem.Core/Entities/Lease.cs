namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class Lease
{
    public int LeaseId { get; set; }

    public int UnitId { get; set; }

    public int TenantId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal MonthlyRent { get; set; }

    public decimal? SecurityDeposit { get; set; }

    public string LeaseStatus { get; set; } = null!;

    public DateOnly ContractDate { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<RentCharge> RentCharges { get; set; } = new List<RentCharge>();

    public virtual ICollection<RentPayment> RentPayments { get; set; } = new List<RentPayment>();

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
