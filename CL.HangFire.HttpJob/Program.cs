using Hangfire;
using Hangfire.HttpJob;
using Hangfire.Console;
using Hangfire.SqlServer;
using Hangfire.Dashboard.BasicAuthorization;

var builder = WebApplication.CreateBuilder(args);
var Config = builder.Configuration;

builder.Services.AddHangfire(options =>
{
    options.UseStorage(new SqlServerStorage(Config["ConnectionStrings:Default"], new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    })).UseConsole().UseHangfireHttpJob();
});


var app = builder.Build();

//app.UseHangfireServer();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
            {
                RequireSsl = false,
                SslRedirect = false,
                LoginCaseSensitive = true,
                Users = new []
                {
                    new BasicAuthAuthorizationUser
                    {
                        Login = "admin",
                        PasswordClear =  "test"
                    }
                }
            })}
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("ok.");
});

app.Run();
