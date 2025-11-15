using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public class RequestInfo : IRequestInfo
{
    public RequestInfo()
    {
    }
    //private RequestInfo(long userId, UserRoles role)
    //{
    //    UserId = userId;
    //    Role = role;
    //}
    //public static RequestInfo GetInstance(long userId, UserRoles role) => new RequestInfo(userId, role);

    public long UserId { get; set; }

    public UserRoles Role { get; set; }
}