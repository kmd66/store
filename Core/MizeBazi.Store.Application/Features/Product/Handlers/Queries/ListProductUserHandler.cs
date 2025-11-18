using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Application;

public class ListProductUserHandler(
    IProductRead dataSource
    ) : QueryBase<ListProductUserQuery, ListProductUserResult>
{

    public override Task<ListProductUserResult> Handle(ListProductUserQuery query)
        => dataSource.ListAsync(query);
}