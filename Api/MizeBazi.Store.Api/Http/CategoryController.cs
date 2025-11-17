using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;
public class CategoryController(IAppMediator mediator) : _ControllerBase
{
    /// <summary>
    /// Admin  اکشن های مدیریت
    /// </summary>
    [HttpPost("add")]
    public Task<Result> Add([FromBody] AddCategoryCommand model)
        => mediator.Send(model);

    [HttpPost("edite")]
    public Task<Result> Edite([FromBody] EditeCategoryCommand model)
        => mediator.Send(model);

    [HttpPost("get")]
    public Task<Result<GetCategoryResult>> Get([FromBody] GetCategoryQuery model)
        => mediator.Send(model);

    [HttpPost("list")]
    public Task<Result<IEnumerable<ListCategoryResult>>> List([FromBody] ListCategoryQuery model)
        => mediator.Send(model);

    /// <summary>
    /// Guest  اکشن های کاربر
    /// </summary>
    [AllowAnonymous, HttpPost("getForUser")]
    public Task<GetCategoryForUserResult> GetForUser([FromBody] GetCategoryQuery model)
        => mediator.Pipline<GetCategoryForUserResult>(model);

    [AllowAnonymous, HttpPost("listForUser")]
    public Task<ListCategoryForUserResult> ListForUser([FromBody] ListCategoryQuery model)
        => mediator.Pipline<ListCategoryForUserResult>(model);
}
