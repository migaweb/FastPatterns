using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.Infrastructure.Configuration;
using FastPatterns.Mediator.WebApiSample.Behaviors;
using FastPatterns.Mediator.WebApiSample.Features.GetWeather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediator()
                .AddPipeline(typeof(LoggingPipelineBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", async (IMediator mediator) =>
{
  return await mediator.SendAsync(new GetWeatherQuery());
})
.WithName("GetWeatherForecast");

app.Run();

