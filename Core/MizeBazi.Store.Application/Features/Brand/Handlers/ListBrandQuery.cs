using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListBrandQueryHandler(
    IBrandRead dataSource
    ) : QueryBase<ListBrandQuery, Result<IEnumerable<ListBrandResult>>>
{

    public override Task<Result<IEnumerable<ListBrandResult>>> Handle(ListBrandQuery query)
        => dataSource.ListAsync(query);
}