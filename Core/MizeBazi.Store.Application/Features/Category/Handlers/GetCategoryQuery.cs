using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class GetCategoryQueryHandler(
    ICategoryRead dataSource,
    IAppLogger<GetCategoryQueryHandler> logger
    ) : QueryBase<GetCategoryQuery, Result<GetCategoryResult>>
{

    public override Task<Result<GetCategoryResult>> Handle(GetCategoryQuery query){
        query.Check(logger, BrandConstants.ValidatError_Id);
        return dataSource.GetAsync(query);
    }
}