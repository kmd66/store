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
    /// Admin
    ///  اکشن های مدیریت
    /// </summary>
    [Auth(UserRoles.Admin), HttpPost("add")]
    public Task<Result> Add([FromBody] AddBanerCommand model)
        => mediator.Send(model);

    [Auth(UserRoles.Admin), HttpPost("edite")]
    public Task<Result> Edite([FromBody] EditeBanerCommand model)
        => mediator.Send(model);

    [Auth(UserRoles.Admin), HttpPost("get")]
    public Task<Result<GetBanerResult>> Get([FromBody] GetBanerQuery model)
        => mediator.Send(model);

    [Auth(UserRoles.Admin), HttpPost("list")]
    public Task<Result<IEnumerable<ListBanerResult>>> List([FromBody] ListBanerQuery model)
        => mediator.Send(model);

    /// <summary>
    /// Guest
    ///  اکشن های کاربر
    /// </summary>
    /// 
    [Auth(UserRoles.Guest), HttpPost("getForUser")]
    public Task<Result<GetBanerForUserResult>> GetForUser([FromBody] GetBanerQuery model)
        => mediator.Pipline(model, new GetBanerForUserResult());

    [Auth(UserRoles.Guest), HttpPost("listForUser")]
    public Task<Result<IEnumerable<ListBanerForUserResult>>> ListForUser([FromBody] ListBanerQuery model)
        => mediator.Pipline(model, new ListBanerForUserResult());
}
