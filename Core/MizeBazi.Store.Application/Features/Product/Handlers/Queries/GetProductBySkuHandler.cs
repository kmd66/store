using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class GetProductBySkuHandler(
    IProductRead dataSource
    //IAppLogger<GetProductBySkuHandler> logger
    ) : QueryBase<GetProductBySkuQuery, GetProductResult>
{

    public override async Task<GetProductResult> Handle(GetProductBySkuQuery query)
    {
        try
        {
            var sku = Sku.From(query.sku);
            return await dataSource.GetAsync(null, null, sku.Value); 
        }
        catch (ValidatorException ex)
        {
            //logger.LogWarning($"SKU validation failed: {query.sku} - {ex.Message}");
            return (GetProductResult)ResultRecord.Failure(ex.Message);
        }
    }
}