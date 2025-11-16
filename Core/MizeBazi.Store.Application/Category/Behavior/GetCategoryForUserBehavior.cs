using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class GetCategoryForUserBehavior : PipelineBase<
    Result<GetCategoryResult>,
    GetCategoryForUserResult
>
{
    public override Task<GetCategoryForUserResult> Handle(Result<GetCategoryResult> model)
    {
        var data = model.JsonMapObject<GetCategoryForUserResult>();
        return Task.FromResult(data);
    }
}