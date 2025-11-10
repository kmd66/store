namespace MizeBazi.Store.Common.Shared;

public enum OrderStatus : byte
{
    Pending = 1,    // در انتظار پرداخت
    Processing = 2, // در حال پردازش
    Shipped = 3,    // ارسال شده
    Delivered = 4,  // تحویل داده شده
    Cancelled = 5,  // لغو شده
    Refunded = 6    // مرجوع شده
}

public enum PaymentStatus : byte
{
    Pending = 1,    // در انتظار پرداخت
    Processing = 2, // در حال پردازش
    Completed = 3,  // موفق
    Failed = 4,     // ناموفق
    Refunded = 5,   // بازپرداخت شده
    Cancelled = 6   // لغو شده
}

public enum PaymentMethod : byte
{
    Online = 1,
    CashOnDelivery = 2, // پرداخت در محل
    BankTransfer = 3
}