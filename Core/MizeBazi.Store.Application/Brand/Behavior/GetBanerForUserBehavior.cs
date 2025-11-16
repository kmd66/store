using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class GetBanerForUserBehavior : PipelineBase<
    Result<GetBanerResult>,
    GetBanerForUserResult
>
{
    public override Task<GetBanerForUserResult> Handle(Result<GetBanerResult> model)
    {
        var data = model.JsonMapObject<GetBanerForUserResult>();
        return Task.FromResult(data);
    }
}