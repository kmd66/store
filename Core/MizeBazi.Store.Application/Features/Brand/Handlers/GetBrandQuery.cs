using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class GetBrandQueryHandler(
    IBrandRead dataSource,
    IAppLogger<GetBrandQueryHandler> logger
    ) : QueryBase<GetBrandQuery, Result<GetBrandResult>>
{

    public override Task<Result<GetBrandResult>> Handle(GetBrandQuery query){
        query.Check(logger, BrandConstants.ValidatError_Id);
        return dataSource.GetAsync(query);
    }
}