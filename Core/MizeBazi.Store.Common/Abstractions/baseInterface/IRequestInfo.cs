
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Common.Abstractions;

public interface IRequestInfo
{
    public long UserId { get; }

    public UserRoles Role { get;}

}
