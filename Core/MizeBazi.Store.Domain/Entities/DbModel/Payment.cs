using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Entities;

public class DbPayment : SoftDeleteEntity
{
    public long OrderId { get; set; }

    public decimal Amount { get; set; }
}