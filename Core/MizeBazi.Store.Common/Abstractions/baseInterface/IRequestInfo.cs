
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Common.Abstractions;

public interface IRequestInfo
{
    public long UserId { get; set; }

    public UserRoles Role { get; set; }

}
