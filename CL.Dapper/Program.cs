using CL.Dapper.DapperHelper;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
AppSettings.Init(configuration);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
