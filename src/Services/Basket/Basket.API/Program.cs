using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

// add services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(confg =>
{
    confg.RegisterServicesFromAssembly(assembly);
    confg.AddOpenBehavior(typeof(ValidatoinBehaviour<,>));
    confg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();


builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>(); Cant do this line bveacuse it will chooose this 
//neglect the first one  so we gonna need decorator 
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});


//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

//Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

//Cross Cutting Services


builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
.AddRedis(builder.Configuration.GetConnectionString("Redis"));



var app = builder.Build();

// configure http request pipeline

app.MapCarter();
app.UseExceptionHandler(options => { });
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
