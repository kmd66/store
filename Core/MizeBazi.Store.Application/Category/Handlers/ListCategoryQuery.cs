using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;


public class ListCategoryQueryHandler(
    ICategoryRead dataSource
    ) : QueryBase<ListCategoryQuery, Result<IEnumerable<ListCategoryResult>>>
{

    public override Task<Result<IEnumerable<ListCategoryResult>>> Handle(ListCategoryQuery query)
        => dataSource.ListAsync(query);
}