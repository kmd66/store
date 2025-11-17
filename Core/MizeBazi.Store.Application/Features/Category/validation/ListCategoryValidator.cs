using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Constants;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListCategoryValidator(
    IAppLogger<ListCategoryQuery> logger,
    IRequestInfo requestInfo
) 
    : BehaviorBase<ListCategoryQuery>
{
    public override Task Handle(ListCategoryQuery query)
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