using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;
public class ProductController(IAppMediator mediator) : _ControllerBase
{
    /// <summary>
    /// Admin  اکشن های مدیریت
    /// </summary>
    [HttpPost("add")]
    public Task<Result<long>> Add([FromBody] AddProductCommand model, CancellationToken ct)
        => mediator.Send(model, ct);

    [HttpPost("edite")]
    public Task<Result> Edite([FromBody] EditeProductCommand model, CancellationToken ct)
        => mediator.Send(model, ct);

    [HttpPost("editeCategories")]
    public Task<Result> EditeCategories([FromBody] EditeProductCategoryCommand model, CancellationToken ct)
        => mediator.Send(model, ct);

    [HttpPost("editePublish")]
    public Task<Result> EditePublish([FromBody] PublishProductCommand model)
        => mediator.Send(model);

    [HttpPost("delete")]
    public Task<Result> Delete([FromBody] DeleteProductCommand model)
        => mediator.Send(model);



    [HttpPost("getById")]
    public Task<GetProductResult> GetById([FromBody] GetProductByIdQuery model)
        => mediator.Send(model);

    [HttpPost("getBySku")]
    public Task<GetProductResult> GetBySku([FromBody] GetProductBySkuQuery model)
        => mediator.Send(model);

    [HttpPost("list")]
    public Task<ListProductResult> List([FromBody] ListProductQuery model)
        => mediator.Send(model);

    /// <summary>
    /// Guest  اکشن های کاربر
    /// </summary>
    [AllowAnonymous, HttpPost("getByIdForUser")]
    public Task<GetProductUserResult> GetByIdForUser([FromBody] GetProductByIdQuery model)
        => mediator.Pipline<GetProductUserResult>(model);

    [AllowAnonymous, HttpPost("getBySkuForUser")]
    public Task<GetProductUserResult> GetBySkuForUser([FromBody] GetProductBySkuQuery model)
        => mediator.Pipline<GetProductUserResult>(model);

    [AllowAnonymous, HttpPost("listForUser")]
    public Task<ListProductUserResult> ListForUser([FromBody] ListProductUserQuery model)
        => mediator.Send(model);
}

