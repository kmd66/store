using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly AutoEventDispatcher _dispatcher;

    public ValuesController(AutoEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    [HttpPost]
    public void Post([FromBody] string value)
    {
        new OrderService(_dispatcher).ConfirmOrder(new Domain.DbBrand());
    }
}
