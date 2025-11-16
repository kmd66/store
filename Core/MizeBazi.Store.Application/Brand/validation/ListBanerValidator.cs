using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Constants;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListBanerValidator(
    IAppLogger<ListBanerQuery> logger,
    IRequestInfo requestInfo
) 
    : BehaviorBase<ListBanerQuery>
{
    public override Task Handle(ListBanerQuery query)
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