using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data;

public class DbPayment : SoftDeleteEntity
{
    public long OrderId { get; set; }

    public decimal Amount { get; set; }
}