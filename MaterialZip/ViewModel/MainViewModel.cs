using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialZip.Model.Entities;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.ViewModel;

public partial class MainViewModel : ViewModelBase
{
    private const string DefaultLogicalDrivesPath = "/";

    private ILogger _logger;
    private IExplorer _explorer;
    private IExplorerHistory _history;
    private ILastDirectoryBuffer _buffer;
    
    [ObservableProperty] private string _currentPath;
    [ObservableProperty] private IEnumerable<FileEntity> _entities;
    [ObservableProperty] private bool _isDataGridVisible = true; 
    
    public MainViewModel(
        ILastDirectoryBuffer buffer,
        ILogger logger,
        IExplorer explorer,
        IExplorerHistory history)
    {
        _buffer = buffer;
        _logger = logger;
        _explorer = explorer;
        _history = history;
        
        var directory = buffer.FromBuffer();
        SetDirectoryContent(directory);
    }

    
    [RelayCommand]
    private async Task SetDirectoryContent(FileEntity directory)
    {
        if (directory.Path == DefaultLogicalDrivesPath)
            Entities = await _explorer.GetLogicalDrivesAsync();
        else
            Entities = await _explorer.GetDirectoryContentAsync(directory);
    }

    
    
}