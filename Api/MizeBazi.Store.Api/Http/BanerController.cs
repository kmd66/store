using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Api.Middleware;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;
public class BanerController(IAppMediator mediator) : _ControllerBase
{

    /// <summary>
    /// Admin  اکشن های مدیریت
    /// </summary>
    [HttpPost("add")]
    public Task<Result> Add([FromBody] AddBanerCommand model)
        => mediator.Send(model);

    [HttpPost("edite")]
    public Task<Result> Edite([FromBody] EditeBanerCommand model)
        => mediator.Send(model);

    [HttpPost("get")]
    public Task<Result<GetBanerResult>> Get([FromBody] GetBanerQuery model)
        => mediator.Send(model);

    [HttpPost("list")]
    public Task<Result<IEnumerable<ListBanerResult>>> List([FromBody] ListBanerQuery model)
        => mediator.Send(model);

    /// <summary>
    /// Guest  اکشن های کاربر
    /// </summary>
    [AllowAnonymous, HttpPost("getForUser")]
    public Task<Result<GetBanerForUserResult>> GetForUser([FromBody] GetBanerQuery model)
        => mediator.Pipeline<Result<GetBanerForUserResult>>(model);

    [Auth(UserRoles.Guest), HttpPost("listForUser")]
    public Task<Result<IEnumerable<ListBanerForUserResult>>> ListForUser([FromBody] ListBanerQuery model)
        => mediator.Pipeline<Result<IEnumerable<ListBanerForUserResult>>>(model);
}
