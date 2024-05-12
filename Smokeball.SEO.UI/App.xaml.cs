using System.IO;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Smokeball.SEO.Services;

namespace Smokeball.SEO.UI;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    public static IConfiguration? Config { get; private set; }

    public App()
    {
        Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.AddSingleton<MainWindow>();
                services.AddHttpClient();
                services.AddTransient<ICheckSeoService, CheckSeoService>();

                services.Configure<AppSettings>(Config.GetSection("AppSettings"));
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
