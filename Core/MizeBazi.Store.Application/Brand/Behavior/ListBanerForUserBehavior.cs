using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListBanerForUserBehavior : PipelineBase<
    Result<IEnumerable<ListBanerResult>>,
    ListBanerForUserResult
>
{
    public override Task<ListBanerForUserResult> Handle(Result<IEnumerable<ListBanerResult>> model)
    {
        var data = model.JsonMapObject<ListBanerForUserResult>();
        return Task.FromResult(data);
    }
}