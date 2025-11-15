using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;


public class ListBanerQueryHandler(
    IBrandRead dataSource
    ) : QueryBase<ListBanerQuery, Result<IEnumerable<ListBanerResult>>>
{

    public override Task<Result<IEnumerable<ListBanerResult>>> Handle(ListBanerQuery query)
        => dataSource.ListAsync(query);
}