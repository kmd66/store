using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;

public class BanerController(IAppMediator mediator) : _ControllerBase
{

    [HttpPost("add")]
    public Task<Result> Add([FromBody] AddBanerCommand model)
        => mediator.Send(model);

    [HttpPost("edite")]
    public Task<Result> Edite([FromBody] EditeBanerCommand model)
        => mediator.Send(model);

    [HttpPost("get")]
    public Task<Result<BaseBanerRecordModel>> Get([FromBody] GetBanerQuery model)
        => mediator.Send(model);

    [HttpPost("list")]
    public Task<Result<IEnumerable<ListBanerResult>>> List([FromBody] ListBanerQuery model)
        => mediator.Send(model);
}
