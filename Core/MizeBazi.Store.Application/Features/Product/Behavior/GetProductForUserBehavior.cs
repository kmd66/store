using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Application;

public class GetProductForUserBehavior : PipelineBase<
    GetProductResult,
    GetProductUserResult
>
{
    public override Task<GetProductUserResult> Handle(GetProductResult model)
    {
        var data = model.JsonMapObject<GetProductUserResult>();
        return Task.FromResult(data);
    }
}