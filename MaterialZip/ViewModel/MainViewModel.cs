using CommunityToolkit.Mvvm.ComponentModel;
using MaterialZip.Model.Entities;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.ViewModel;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _currentPath;
    [ObservableProperty] private IReadOnlyCollection<FileEntity> _entities;
    [ObservableProperty] private bool _isDataGridVisible = true; 
    
    
    public MainViewModel(ILastDirectoryBuffer buffer,
        ILogger logger,
        IExplorer explorer,
        IExplorerHistory history)
    {
        _currentPath = buffer.FromBuffer();
        _entities = explorer.GetLogicalDrives();
    }
    
    
}