using System.Diagnostics;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialDesignThemes.Wpf;
using MaterialZip.Bootstrapping;
using MaterialZip.Bootstrapping.Abstractions;
using MaterialZip.DIExtensions;
using MaterialZip.Options;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.View;
using MaterialZip.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace MaterialZip;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string ApplicationStartedLogMessage = "Application have been just started";
    private const string ApplicationOnExitLogMessage = "Application was stoped with exit code {code}";
    
    private readonly IBootstrapper _app;
    
    public App()
    {
        var builder = Bootstrapper.CreateBuilder();
        builder.Configuration
            .AddJsonFile("configuration.json", optional: true, reloadOnChange: true);
        builder.Services
            .Configure<ApplicationOptions>(builder.Configuration.GetSection(nameof(ApplicationOptions)))
            .AddScoped<PaletteHelper>()
            .AddSingleton<ViewModelLocator>()
            .AddLastDirectoryManagers()
            .AddThemeLoader()
            .AddScoped<MainViewModel>()
            .AddScoped<MainView>()
            .AddExplorer();
        builder.Logging
            .ReadFrom.Configuration(builder.Configuration);
        _app = builder.CreateBootstrapper();
        Ioc.Default.ConfigureServices(_app.Services);
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        using (var scope = _app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<IThemeLoader>().LoadTheme();
            var lastDirectoryGetter = scope.ServiceProvider.GetRequiredService<ILastDirectoryGetter>();
            var buffer = scope.ServiceProvider.GetRequiredService<ILastDirectoryBuffer>();
            buffer.ToBuffer(lastDirectoryGetter.LastDirectory);
            scope.ServiceProvider.GetRequiredService<MainView>().Show();
        }
        _app.Logging.Debug(ApplicationStartedLogMessage);
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        var buffer =  _app.Services.GetRequiredService<ILastDirectoryBuffer>();
        using(var scope = _app.Services.CreateScope())
            scope.ServiceProvider.GetRequiredService<ILastDirectoryChanger>()
                .ChangeLastDirectory(buffer.FromBuffer());
        _app.Logging.Information(ApplicationOnExitLogMessage, e.ApplicationExitCode);
        base.OnExit(e);
    }
}

