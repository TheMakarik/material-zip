using System.Windows;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialZip.Bootstrapping;
using MaterialZip.DIExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialZip;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var builder = Bootstrapper.CreateBuilder();
        builder.Configuration
            .AddJsonFile("configuration.json");
        builder.Services
            .AddExplorer();
        builder.Logging
            .ReadFrom.Configuration(builder.Configuration);
        var app = builder.CreateBootstrapper();
    }
}

