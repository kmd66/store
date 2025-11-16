using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListBrandForUserBehavior : PipelineBase<
    Result<IEnumerable<ListBrandResult>>,
    ListBrandForUserResult
>
{
    public override Task<ListBrandForUserResult> Handle(Result<IEnumerable<ListBrandResult>> model)
    {
        var data = model.JsonMapObject<ListBrandForUserResult>();
        return Task.FromResult(data);
    }
}