using System.Windows;
using System.Windows.Data;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialZip.Bootstrapping;
using MaterialZip.Bootstrapping.Abstractions;
using MaterialZip.DIExtensions;
using MaterialZip.Factories;
using MaterialZip.Factories.Abstractions;
using MaterialZip.Model.Enums;
using MaterialZip.Options;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialZip;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string ApplicationStartedLogMessage = "Application have been just started";
    private const string ApplicationOnExitLogMessage = "Application was stoped with exit cose {code}";
    
    private readonly IBootstrapper _app;
    
    public App()
    {
        var builder = Bootstrapper.CreateBuilder();
        builder.Configuration
            .AddJsonFile("configuration.json");
        builder.Services
            .Configure<ApplicationOptions>(builder.Configuration.GetSection(nameof(ApplicationOptions)))
            .AddSingleton<IPaletteHelperFactory, PaletteHelperFactory>()
            .AddThemeLoader()
            .AddExplorer();
        builder.Logging
            .ReadFrom.Configuration(builder.Configuration);
        _app = builder.CreateBootstrapper();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        using (var scope = _app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<IThemeLoader>().LoadTheme();
        }
        
        _app.Logging.Debug(ApplicationStartedLogMessage);
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _app.Logging.Information(ApplicationOnExitLogMessage, e.ApplicationExitCode);
        base.OnExit(e);
    }
}

