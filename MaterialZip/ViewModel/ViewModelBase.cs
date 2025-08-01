using CommunityToolkit.Mvvm.ComponentModel;
using MaterialZip.Services.LocalizationServices.Abstractions;

namespace MaterialZip.ViewModel;

/// <summary>
/// Base view model for every view model instance
/// </summary>
public partial class ViewModelBase(ILocalizationProvider localization) : ObservableObject
{
    [ObservableProperty] private ILocalizationProvider _localization = localization;
}