using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;


public class ListBanerQueryHandler(
    IBrandRead dataSource
    ) : IQueryHandler<ListBanerQuery, Result<IEnumerable<ListBanerResult>>>
{

    public Task<Result<IEnumerable<ListBanerResult>>> Handle(ListBanerQuery query)
        => dataSource.ListAsync(query);

    public Task<Result<IEnumerable<ListBanerResult>>> Handle(ListBanerQuery query, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}