using MizeBazi.Store.Api.Helper;
using MizeBazi.Store.Api.Middleware;
using MizeBazi.Store.Common.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();

builder.Configuration.SetAppSetings();
builder.SetLogRegistration();
builder.Services.AddServices();
builder.SetGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
AppSetings.IsDevelopment = app.Environment.IsDevelopment();
if (AppSetings.IsDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseRouting();
app.MapControllers();

app.MapFallback(() => Results.NotFound("Endpoint not found"));
app.SetAppMiddlewares();
app.Run();
