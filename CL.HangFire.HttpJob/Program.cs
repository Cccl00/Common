using Hangfire;
using Hangfire.HttpJob;
using Hangfire.Console;
using Hangfire.SqlServer;
using Hangfire.Dashboard.BasicAuthorization;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
var Config = builder.Configuration;

builder.Services.AddHangfire(options =>
{
    options
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        //.UseStorage(new SqlServerStorage(Config["ConnectionStrings:Default"], new SqlServerStorageOptions
        .UseSqlServerStorage(Config.GetConnectionString("Default"), new SqlServerStorageOptions  // 任选这行与上面一行其一种配置方式都可以。
        //如果没有在数据库里预先创建一个配置文件对应的DB的话 会报错！ 启动程序会判断有没有叫配置文件里配置的db。如果存在但没有表会初始化hangfire的表！
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),               // 命令批处理最大超时时间
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),           // 滑动超时时间
            QueuePollInterval = TimeSpan.Zero,                              // 作业队列轮询间隔。默认值为15秒
            UseRecommendedIsolationLevel = true,                            // 是否使用建议的隔离级别
            DisableGlobalLocks = true,                                      // 是否禁用全局锁,需要迁移到模式7
        }).UseConsole().UseHangfireHttpJob();
});

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "定时任务调度管理器",
    Authorization = new[]
    {
        new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false, // 是否启用ssl验证，即https
            SslRedirect = false,
            LoginCaseSensitive = true,
            Users = new[]
            {
                new BasicAuthAuthorizationUser
                {
                    Login = "admin",
                    PasswordClear = "test"
                }
            }
        })
    }
});

app.Run(async (context) => { await context.Response.WriteAsync("ok."); });

app.Run();