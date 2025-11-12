
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;


public class OrderService(AutoEventDispatcher dispatcher)
{
    private readonly AutoEventDispatcher _dispatcher = dispatcher;

    public async Task ConfirmOrder(DbBrand model)
    {
        var brand = Brand.CreateFromBaseModel(model);
        await _dispatcher.ExecuteEvents(brand, EventType.Add);
    }
}