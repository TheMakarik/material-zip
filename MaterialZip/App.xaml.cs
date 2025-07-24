using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialDesignThemes.Wpf;
using MaterialZip.Bootstrapping;
using MaterialZip.Bootstrapping.Abstractions;
using MaterialZip.DIExtensions;
using MaterialZip.Model.Entities;
using MaterialZip.Options;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.WindowsFunctions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using MaterialZip.View;
using MaterialZip.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace MaterialZip;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
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
            .AddLastDirectoryManagers()
            .AddThemeLoader()
            .AddIconExtractor()
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
            LoadTheme(scope);
            SetBuffer(scope);
            ShowWindow(scope);
        }
        _app.Logging.Debug(ApplicationStartedLogMessage);
        base.OnStartup(e);
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        var buffer =  _app.Services.GetRequiredService<ILastDirectoryBuffer>();
        using(var scope = _app.Services.CreateScope())
            scope.ServiceProvider.GetRequiredService<ILastDirectoryChanger>()
                .ChangeLastDirectory(buffer.FromBuffer().Path);
        _app.Logging.Information(ApplicationOnExitLogMessage, e.ApplicationExitCode);
        base.OnExit(e);
    }

    private void LoadTheme(IServiceScope scope)
    {
       scope.ServiceProvider.GetRequiredService<IThemeLoader>().LoadTheme();
    }
    
    private void SetBuffer(IServiceScope scope)
    {
        var lastDirectoryGetter = scope.ServiceProvider.GetRequiredService<ILastDirectoryGetter>();
        var buffer = scope.ServiceProvider.GetRequiredService<ILastDirectoryBuffer>();
        buffer.ToBuffer(new FileEntity(lastDirectoryGetter.LastDirectory, true));
    }
    
    private void ShowWindow(IServiceScope scope)
    {
        scope.ServiceProvider.GetRequiredService<MainView>().Show();
    }
    
    

}

