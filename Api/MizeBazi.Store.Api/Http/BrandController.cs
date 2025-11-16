using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Api.Middleware;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;
public class BrandController(IAppMediator mediator) : _ControllerBase
{
    /// <summary>
    /// Admin  اکشن های مدیریت
    /// </summary>
    [HttpPost("add")]
    public Task<Result> Add([FromBody] AddBrandCommand model)
        => mediator.Send(model);

    [HttpPost("edite")]
    public Task<Result> Edite([FromBody] EditeBrandCommand model)
        => mediator.Send(model);

    [HttpPost("get")]
    public Task<Result<GetBrandResult>> Get([FromBody] GetBrandQuery model)
        => mediator.Send(model);

    [HttpPost("list")]
    public Task<Result<IEnumerable<ListBrandResult>>> List([FromBody] ListBrandQuery model)
        => mediator.Send(model);

    /// <summary>
    /// Guest  اکشن های کاربر
    /// </summary>
    [AllowAnonymous, HttpPost("getForUser")]
    public Task<GetBrandForUserResult> GetForUser([FromBody] GetBrandQuery model)
        => mediator.Pipeline<GetBrandForUserResult>(model);

    [Auth(UserRoles.Guest), HttpPost("listForUser")]
    public Task<ListBrandForUserResult> ListForUser([FromBody] ListBrandQuery model)
        => mediator.Pipeline<ListBrandForUserResult>(model);
}
