using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain.Entities;

public class Brand : DbBrand
{
    private Brand() { } // For EF Core

    public Brand(string firstName, string lastName, string email)
    {
    }
    public override void Confirm()
    {
        if (IsConfirmed) return;
        base.Confirm();
    }
}

