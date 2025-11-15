using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListBanerForUserBehavior : PipelineBase<
    Result<IEnumerable<ListBanerResult>>, 
    Result<IEnumerable<ListBanerForUserResult>>
>
{
    public override Task<Result<IEnumerable<ListBanerForUserResult>>> Handle(Result<IEnumerable<ListBanerResult>> model)
    {
        if (!model.success)
            return Result<IEnumerable<ListBanerForUserResult>>.FailureAsync(message: model.message);
        var data = model.data.ToList().JsonMapObject<IEnumerable<ListBanerForUserResult>>();
        return Result<IEnumerable<ListBanerForUserResult>>.SuccessfulAsync(data: data);
    }
}