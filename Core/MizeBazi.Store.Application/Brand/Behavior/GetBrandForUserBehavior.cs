using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class GetBrandForUserBehavior : PipelineBase<
    Result<GetBrandResult>,
    GetBrandForUserResult
>
{
    public override Task<GetBrandForUserResult> Handle(Result<GetBrandResult> model)
    {
        var data = model.JsonMapObject<GetBrandForUserResult>();
        return Task.FromResult(data);
    }
}