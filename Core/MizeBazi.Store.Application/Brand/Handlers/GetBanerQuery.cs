using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class GetBanerQueryHandler(
    IBrandRead dataSource,
    IAppLogger<GetBanerQueryHandler> logger
    ) : IQueryHandler<GetBanerQuery, Result<GetBanerResult>>
{

    public Task<Result<GetBanerResult>> Handle(GetBanerQuery query){

        if (query.Id == null && query.UnicId == null)
        {
            logger.LogWarning($"Exception message: {BanerConstants.ValidatError_Id}");
            throw new ValidatorException($"Brand Get Exception: {BanerConstants.ValidatError_Id}");
        }
        
        return dataSource.GetAsync(query);
    }

    public Task<Result<GetBanerResult>> Handle(GetBanerQuery query, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}