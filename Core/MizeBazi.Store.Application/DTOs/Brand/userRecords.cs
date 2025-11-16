using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record BaseUserBrand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string LogoUrl { get; init; }
    public Guid UnicId { get; init; }
}
public record GetBrandForUserResult : ResultRecord
{
    public BaseUserBrand data { get; init; }
};
public record ListBrandForUserResult : ResultRecord
{
    public IEnumerable<BaseUserBrand> data { get; init; }
    public int TotalCount { get; init; }
};
