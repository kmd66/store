using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class GetBanerForUserBehavior : PipelineBase<
    Result<GetBanerResult>,
    Result<GetBanerForUserResult>
>
{
    public override Task<Result<GetBanerForUserResult>> Handle(Result<GetBanerResult> model)
    {
        if (!model.success)
            return Result<GetBanerForUserResult>.FailureAsync(message: model.message);
        var data = model.data.JsonMapObject<GetBanerForUserResult>();
        return Result<GetBanerForUserResult>.SuccessfulAsync(data: data);
    }
}