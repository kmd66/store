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
public record GetBanerForUserResult : BaseUserBaner, IRequest<Result<GetBanerForUserResult>>;
public record ListBanerForUserResult : BaseUserBaner, IRequest<Result<IEnumerable<ListBanerForUserResult>>>
{
    public int TotalCount { get; init; }
};
