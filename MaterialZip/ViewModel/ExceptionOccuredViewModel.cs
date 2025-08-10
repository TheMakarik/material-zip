using System.Runtime.InteropServices;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf.Converters;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.LocalizationServices.Abstractions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace MaterialZip.ViewModel;

public sealed partial class ExceptionOccuredViewModel(ILocalizationProvider localization, ILogPathOpener logPathOpener, ILogger<ExceptionOccuredViewModel> logger) : ViewModelBase(localization)
{
    private const string ExceptionStringWasCopiedLogMessage = "{exceptionString} was copied";
    
    [ObservableProperty] private string _exceptionString = string.Empty;
    [ObservableProperty] private bool _isPopupVisible = false;
    
    [RelayCommand]
    private void Close(Window window)
    {
       window.Close();
    }

    [RelayCommand]
    private void OpenLogs()
    {
        logPathOpener.Open();
    }

    [RelayCommand]
    private async Task CopyTrace()
    {
        Clipboard.SetText(ExceptionString);
        logger.LogDebug(ExceptionStringWasCopiedLogMessage, ExceptionString);
        IsPopupVisible = true;
        await Task.Delay(TimeSpan.FromSeconds(2));
        IsPopupVisible = false;
      
    }
}