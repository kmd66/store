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

        if (query.Id == null && query.UnicId == null)
        {
            logger.LogWarning($"Exception message: {BrandConstants.ValidatError_Id}");
            throw new ValidatorException($"Brand Get Exception: {BrandConstants.ValidatError_Id}");
        }
        
        return dataSource.GetAsync(query);
    }
}