using NLog;
using NLog.Web;
using WebApi;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    logger.Debug("init Main");

try
{
    var webApplicationOptions = new WebApplicationOptions() 
    { 
        ContentRootPath = AppContext.BaseDirectory, 
        Args = args, 
        ApplicationName = System.Diagnostics.Process.GetCurrentProcess().ProcessName 
    };


    var builder = WebApplication.CreateBuilder(webApplicationOptions);

    // Add services to the container.
    var services = builder.Services;
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
    services.AddControllersWithViews();

    services.AddSingleton<Worker>();
    services.AddHostedService<Worker>(provider => provider.GetRequiredService<Worker>());

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Logging.AddEventSourceLogger();


    builder.WebHost.UseNLog();
    builder.WebHost.UseKestrel();

    builder.Host.UseNLog();
    builder.Host.UseWindowsService();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    //app.UseAuthorization();

    // Configure the HTTP request pipeline.

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e);
}
finally
{
    NLog.LogManager.Shutdown();
}

