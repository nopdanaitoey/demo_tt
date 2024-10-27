using demo_tt.Database;
using demo_tt.Middlewares;
using demo_tt.Repositories;
using demo_tt.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IMasterResourceRepository, MasterResourceRepository>();
builder.Services.AddScoped<ITrucksRepository, TrucksRepository>();
builder.Services.AddScoped<IAffectedAreasRepository, AffectedAreasRepository>();
builder.Services.AddScoped<IResourceTrucksRepository, ResourceTrucksRepository>();
builder.Services.AddScoped<ICacheRepository, CacheRepository>();
builder.Services.AddScoped<IAssignmentsRepository, AssignmentsRepository>();



builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

var connection = String.Empty;
var redis = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    redis = builder.Configuration.GetConnectionString("REDIS_CONNECTIONSTRING");

}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
    redis = Environment.GetEnvironmentVariable("REDIS_CONNECTIONSTRING");
}

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connection));
builder.Services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(redis!.ToString()));

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {


app.UseSwagger();

app.UseSwaggerUI();
using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();


await dataContext.SeedDatabase();
// }

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();




app.MapControllers();
app.Run();


