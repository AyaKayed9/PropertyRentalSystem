using Microsoft.EntityFrameworkCore;
using PropertyRentalManagementSystem.Domain.Entities;
namespace PropertyRentalManagementSystem.Infrastructure.Data;

public partial class PropertyRentalManagementDBContext : DbContext
{
    public PropertyRentalManagementDBContext(DbContextOptions<PropertyRentalManagementDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgreementProperty> AgreementProperties { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<MaintenanceExpense> MaintenanceExpenses { get; set; }

    public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

    public virtual DbSet<OwnerSettlement> OwnerSettlements { get; set; }

    public virtual DbSet<OwnerSettlementItem> OwnerSettlementItems { get; set; }

    public virtual DbSet<OwnerSettlementPayment> OwnerSettlementPayments { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyManagementAgreement> PropertyManagementAgreements { get; set; }

    public virtual DbSet<PropertyOwner> PropertyOwners { get; set; }

    public virtual DbSet<PropertyOwnership> PropertyOwnerships { get; set; }

    public virtual DbSet<PropertySubType> PropertySubTypes { get; set; }

    public virtual DbSet<PropertyType> PropertyTypes { get; set; }

    public virtual DbSet<RentCharge> RentCharges { get; set; }

    public virtual DbSet<RentPayment> RentPayments { get; set; }

    public virtual DbSet<RentPaymentAllocation> RentPaymentAllocations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgreementProperty>(entity =>
        {
            entity.HasKey(e => e.AgreementPropertyId).HasName("PK__Agreemen__B0D91D9556739FF1");

            entity.HasIndex(e => new { e.AgreementId, e.PropertyId }, "UQ_AgreementProperties_Agreement_Property").IsUnique();

            entity.Property(e => e.CommissionRate).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Agreement).WithMany(p => p.AgreementProperties)
                .HasForeignKey(d => d.AgreementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgreementProperties_Agreements");

            entity.HasOne(d => d.Property).WithMany(p => p.AgreementProperties)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgreementProperties_Properties");
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.HasKey(e => e.LeaseId).HasName("PK__Leases__21FA58C1A1184C24");

            entity.Property(e => e.ContractDate).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LeaseStatus).HasMaxLength(20);
            entity.Property(e => e.MonthlyRent).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.SecurityDeposit).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Leases)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leases_Tenants");

            entity.HasOne(d => d.Unit).WithMany(p => p.Leases)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leases_Units");
        });

        modelBuilder.Entity<MaintenanceExpense>(entity =>
        {
            entity.HasKey(e => e.MaintenanceExpenseId).HasName("PK__Maintena__2F42FC29F76A020A");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.MaintenanceRequest).WithMany(p => p.MaintenanceExpenses)
                .HasForeignKey(d => d.MaintenanceRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceExpenses_MaintenanceRequests");
        });

        modelBuilder.Entity<MaintenanceRequest>(entity =>
        {
            entity.HasKey(e => e.MaintenanceRequestId).HasName("PK__Maintena__ADBE63E4F8D0A1C5");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasDefaultValue("Medium");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("New");

            entity.HasOne(d => d.Property).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequests_Properties");

            entity.HasOne(d => d.RecordedByUser).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.RecordedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequests_Users");

            entity.HasOne(d => d.ReportedByTenant).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.ReportedByTenantId)
                .HasConstraintName("FK_MaintenanceRequests_Tenants");

            entity.HasOne(d => d.Unit).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_MaintenanceRequests_Units");
        });

        modelBuilder.Entity<OwnerSettlement>(entity =>
        {
            entity.HasKey(e => e.SettlementId).HasName("PK__OwnerSet__7712545AC7B3F8B0");

            entity.HasIndex(e => new { e.OwnerId, e.SettlementPeriod }, "UQ_OwnerSettlements_Owner_Period").IsUnique();

            entity.Property(e => e.NetAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PreviousBalance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalCommissions).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalMaintenanceExpenses).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalManagementFees).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalRentCollected).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnerSettlements)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnerSettlements_PropertyOwners");
        });

        modelBuilder.Entity<OwnerSettlementItem>(entity =>
        {
            entity.HasKey(e => e.SettlementItemId).HasName("PK__OwnerSet__3C08C7C91630E5AC");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ItemType).HasMaxLength(30);

            entity.HasOne(d => d.Property).WithMany(p => p.OwnerSettlementItems)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK_OwnerSettlementItems_Properties");

            entity.HasOne(d => d.Settlement).WithMany(p => p.OwnerSettlementItems)
                .HasForeignKey(d => d.SettlementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnerSettlementItems_OwnerSettlements");
        });

        modelBuilder.Entity<OwnerSettlementPayment>(entity =>
        {
            entity.HasKey(e => e.SettlementPaymentId).HasName("PK__OwnerSet__725023A591AF7DA4");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Settlement).WithMany(p => p.OwnerSettlementPayments)
                .HasForeignKey(d => d.SettlementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnerSettlementPayments_OwnerSettlements");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.Property(e => e.AreaSize).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PropertyStatus).HasMaxLength(30);
            entity.Property(e => e.StreetAddress).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.PropertyType).WithMany(p => p.Properties)
                .HasForeignKey(d => d.PropertyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Properties_PropertTypes");
        });

        modelBuilder.Entity<PropertyManagementAgreement>(entity =>
        {
            entity.HasKey(e => e.AgreementId).HasName("PK__Property__0A3082C3EC192C54");

            entity.Property(e => e.MonthlyManagementFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Owner).WithMany(p => p.PropertyManagementAgreements)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyManagementAgreements_PropertyOwners");
        });

        modelBuilder.Entity<PropertyOwner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Property__819385B8E114C088");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DisplayName).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EntityType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OwnerCategory)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PropertyOwnership>(entity =>
        {
            entity.HasKey(e => new { e.PropertyId, e.OwnerId });

            entity.Property(e => e.OwnershipPercentage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Owner).WithMany(p => p.PropertyOwnerships)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyOwnerships_PropertyOwners");

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyOwnerships)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyOwnerships_Properties");
        });

        modelBuilder.Entity<PropertySubType>(entity =>
        {
            entity.HasKey(e => e.PropertySubTypeId).HasName("PK_PropertySubTypeId");

            entity.HasIndex(e => e.SubTypeName, "UQ_PropertySubTypes_PropertyType_Name").IsUnique();

            entity.Property(e => e.SubTypeName).HasMaxLength(40);

            entity.HasOne(d => d.PropertyType).WithMany(p => p.PropertySubTypes)
                .HasForeignKey(d => d.PropertyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertySubTypeId_PropertyTypes");
        });

        modelBuilder.Entity<PropertyType>(entity =>
        {
            entity.Property(e => e.TypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<RentCharge>(entity =>
        {
            entity.HasKey(e => e.ChargeId).HasName("PK__RentChar__17FC361B04060582");

            entity.HasIndex(e => new { e.LeaseId, e.ChargeMonth }, "UQ_RentCharges_Lease_Month").IsUnique();

            entity.Property(e => e.AmountDue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Lease).WithMany(p => p.RentCharges)
                .HasForeignKey(d => d.LeaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentCharges_Leases");
        });

        modelBuilder.Entity<RentPayment>(entity =>
        {
            entity.HasKey(e => e.RentPaymentId).HasName("PK__RentPaym__1578A94227A58D08");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Lease).WithMany(p => p.RentPayments)
                .HasForeignKey(d => d.LeaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentPayments_Leases");
        });

        modelBuilder.Entity<RentPaymentAllocation>(entity =>
        {
            entity.HasKey(e => e.AllocationId).HasName("PK__RentPaym__B3C6D64B1F05A2E2");

            entity.HasIndex(e => new { e.RentPaymentId, e.ChargeId }, "UQ_RentPaymentAllocations_Payment_Charge").IsUnique();

            entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Charge).WithMany(p => p.RentPaymentAllocations)
                .HasForeignKey(d => d.ChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentPaymentAllocations_RentCharges");

            entity.HasOne(d => d.RentPayment).WithMany(p => p.RentPaymentAllocations)
                .HasForeignKey(d => d.RentPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentPaymentAllocations_RentPayments");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(40);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__Tenants__2E9B47E1207B9281");

            entity.HasIndex(e => new { e.IdentityDocumentType, e.IdentityDocumentNumber }, "UQ_Tenants_IdentityDocument").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(254);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IdentityDocumentNumber).HasMaxLength(50);
            entity.Property(e => e.IdentityDocumentType).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasIndex(e => new { e.PropertyId, e.UnitNumber }, "UQ_Units_Property_UnitNumber").IsUnique();

            entity.Property(e => e.AreaSize).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MonthlyRent).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitNumber).HasMaxLength(20);
            entity.Property(e => e.UnitStatus).HasMaxLength(30);

            entity.HasOne(d => d.Property).WithMany(p => p.Units)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Units_Properties");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users_UserId");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_Users_PhoneNumber").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Roles"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_UserRoles_1");
                        j.ToTable("UserRoles");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
