using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Constants;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListBrandValidator(
    IAppLogger<ListBrandQuery> logger,
    IRequestInfo requestInfo
) 
    : BehaviorBase<ListBrandQuery>
{
    public override Task Handle(ListBrandQuery query)
    {
        if(requestInfo.Role != UserRoles.Admin)
        {
            if (query.PageSize > 100 || query.IsDeleted != true)
            {
                logger.LogError($"{ErrorMsg.SecurityAlertMsg} : model : {query.ToJson()}");
                throw new AppBadRequest();
            }
        }

        return Task.CompletedTask;
    }
} 