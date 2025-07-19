



var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidatoinBehaviour<,>));
    conf.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});


builder.Services.AddValidatorsFromAssembly(assembly);


builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DataBase")!);

}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

// light wieght is the best practice

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// COnfigure the Http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
