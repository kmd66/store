using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Api.Middleware;

namespace MizeBazi.Store.Api.Http;

[ApiController]
[AuthForControllerBase]
[Route("api/v1/[controller]")]
public class _ControllerBase : ControllerBase
{
    //private readonly IRequestInfo _requestInfo;
    public _ControllerBase()
    {
        //_requestInfo = requestInfo;
    }
}