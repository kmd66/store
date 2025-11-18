using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Application;


public class ListProductHandler(
    IProductRead dataSource
    ) : QueryBase<ListProductQuery, ListProductResult>
{

    public override Task<ListProductResult> Handle(ListProductQuery query)
        => dataSource.ListAsync(query);
}