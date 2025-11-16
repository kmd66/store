using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record BaseUserBaner
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string LogoUrl { get; init; }
    public Guid UnicId { get; init; }
}
public record GetBanerForUserResult : ResultRecord
{
    public BaseUserBaner data { get; init; }
};
public record ListBanerForUserResult : ResultRecord
{
    public IEnumerable<BaseUserBaner> data { get; init; }
    public int TotalCount { get; init; }
};
