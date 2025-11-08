using System;
using System.Collections.Generic;
using Tabeebi.Core.Common;
using Tabeebi.Core.Enums;

namespace Tabeebi.Core.Entities;

public class Payment : BaseTenantEntity
{
    // Basic payment information
    public string PaymentNumber { get; set; } = string.Empty; // Unique payment identifier
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";

    // Payment context
    public Guid PatientId { get; set; }
    public virtual UserProfile Patient { get; set; } = null!;

    public Guid? AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }

    // Payment details
    public PaymentType PaymentType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    // Payment processing
    public string? TransactionId { get; set; }
    public string? GatewayReference { get; set; }
    public string? AuthorizationCode { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string? ProcessedBy { get; set; }

    // Description and notes
    public string Description { get; set; } = string.Empty;
    public string? InternalNotes { get; set; }
    public string? ReceiptNotes { get; set; }

    // Refund information
    public decimal RefundAmount { get; set; } = 0;
    public DateTime? RefundDate { get; set; }
    public string? RefundReason { get; set; }
    public string? RefundTransactionId { get; set; }

    // Insurance information
    public bool IsInsuranceClaim { get; set; } = false;
    public Guid? InsuranceProviderId { get; set; }
    public virtual InsuranceProvider? InsuranceProvider { get; set; }
    public string? ClaimNumber { get; set; }
    public decimal InsuranceAmount { get; set; } = 0;
    public decimal PatientResponsibility { get; set; } = 0;

    // Navigation properties
    public virtual ICollection<PaymentItem> Items { get; set; } = new List<PaymentItem>();
    public virtual ICollection<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();
}

// Individual items within a payment
public class PaymentItem : BaseEntity
{
    public Guid PaymentId { get; set; }
    public virtual Payment Payment { get; set; } = null!;

    // Item details
    public string ItemName { get; set; } = string.Empty;
    public string? ItemCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal TotalAmount { get; set; }

    // Item type
    public PaymentItemType ItemType { get; set; }

    // Reference to source entity
    public Guid? ServiceId { get; set; }
    public virtual Service? Service { get; set; }
    public Guid? PrescriptionId { get; set; }
    public virtual Prescription? Prescription { get; set; }
}

// Payment transaction history
public class PaymentTransaction : BaseTenantEntity
{
    public Guid PaymentId { get; set; }
    public virtual Payment Payment { get; set; } = null!;

    // Transaction details
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public TransactionStatus Status { get; set; }

    // Processing information
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public string? TransactionId { get; set; }
    public string? GatewayResponse { get; set; }
    public string? GatewayReference { get; set; }
    public string? ProcessedBy { get; set; }

    // Error handling
    public bool HasError { get; set; } = false;
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }
    public int RetryCount { get; set; } = 0;
    public DateTime? NextRetryAt { get; set; }
}

// Invoice entity for billing
public class Invoice : BaseTenantEntity
{
    // Basic invoice information
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; } = 0;
    public decimal BalanceAmount { get; set; }

    // Invoice status
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public string? Notes { get; set; }
    public string? TermsAndConditions { get; set; }

    // Patient and context
    public Guid PatientId { get; set; }
    public virtual UserProfile Patient { get; set; } = null!;

    public Guid? AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }

    // Billing information
    public string? BillingName { get; set; }
    public string? BillingAddress { get; set; }
    public string? BillingEmail { get; set; }
    public string? BillingPhone { get; set; }

    // Insurance billing
    public bool IsInsuranceBilling { get; set; } = false;
    public Guid? InsuranceProviderId { get; set; }
    public virtual InsuranceProvider? InsuranceProvider { get; set; }
    public string? PolicyNumber { get; set; }
    public string? ClaimNumber { get; set; }
    public DateTime? ClaimSubmittedDate { get; set; }
    public DateTime? ClaimApprovedDate { get; set; }
    public decimal InsuranceApprovedAmount { get; set; } = 0;

    // Navigation properties
    public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

// Invoice items
public class InvoiceItem : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; } = null!;

    // Item details
    public string ItemName { get; set; } = string.Empty;
    public string? ItemCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal TotalAmount { get; set; }

    // Item type
    public InvoiceItemType ItemType { get; set; }

    // Reference to source entity
    public Guid? ServiceId { get; set; }
    public virtual Service? Service { get; set; }
    public Guid? PrescriptionId { get; set; }
    public virtual Prescription? Prescription { get; set; }
}

// Enums for payment and billing
public enum PaymentType
{
    Consultation = 0,
    Procedure = 1,
    Medication = 2,
    LabTest = 3,
    Imaging = 4,
    Supplies = 5,
    Other = 99
}

public enum PaymentMethod
{
    Cash = 0,
    CreditCard = 1,
    DebitCard = 2,
    BankTransfer = 3,
    Check = 4,
    Insurance = 5,
    OnlinePayment = 6,
    MobilePayment = 7,
    Other = 99
}

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5,
    PartiallyRefunded = 6
}

public enum PaymentItemType
{
    Service = 0,
    Medication = 1,
    LabTest = 2,
    Imaging = 3,
    Supplies = 4,
    Procedure = 5,
    Other = 99
}

public enum TransactionType
{
    Payment = 0,
    Refund = 1,
    PartialRefund = 2,
    Chargeback = 3,
    Adjustment = 4
}

public enum TransactionStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5
}

public enum InvoiceStatus
{
    Draft = 0,
    Sent = 1,
    Viewed = 2,
    PartiallyPaid = 3,
    Paid = 4,
    Overdue = 5,
    Cancelled = 6,
    WrittenOff = 7
}

public enum InvoiceItemType
{
    Service = 0,
    Medication = 1,
    LabTest = 2,
    Imaging = 3,
    Supplies = 4,
    Procedure = 5,
    LateFee = 6,
    Interest = 7,
    Other = 99
}