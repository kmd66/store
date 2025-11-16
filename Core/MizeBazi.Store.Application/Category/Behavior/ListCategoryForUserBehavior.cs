using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class ListCategoryForUserBehavior : PipelineBase<
    Result<IEnumerable<ListCategoryResult>>,
    ListCategoryForUserResult
>
{
    public override Task<ListCategoryForUserResult> Handle(Result<IEnumerable<ListCategoryResult>> model)
    {
        var data = model.JsonMapObject<ListCategoryForUserResult>();
        return Task.FromResult(data);
    }
}