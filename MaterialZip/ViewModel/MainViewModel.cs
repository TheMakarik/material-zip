using System.Collections.ObjectModel;
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
    private const string TryingToResetContentFromNullLogMessage = "Trying to reset content from null, method was stopped";
    
    private readonly ILogger _logger;
    private readonly IExplorer _explorer;
    private readonly IExplorerHistory _history;
    private readonly ILastDirectoryBuffer _buffer;
    
    [ObservableProperty] private string _currentPath;
    [ObservableProperty] private FileEntity? _selectedItem;
    [ObservableProperty] private bool _isDataGridVisible = true; 
    [ObservableProperty] private ObservableCollection<FileEntity> _entities = new();
    
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

        var directory = _buffer.FromBuffer();
        ResetDirectoryContent(directory);
    }

    

    [RelayCommand]
    private async Task ResetDirectoryContent(FileEntity directory)
    {
        if (directory.Path == DefaultLogicalDrivesPath)
          AddEntities(await _explorer.GetLogicalDrivesAsync());
        else
           AddEntities(await _explorer.GetDirectoryContentAsync(directory));
        SaveDirectory(directory);
    }



    private void SaveDirectory(FileEntity directory)
    {
        _buffer.ToBuffer(directory);
        _history.CurrentDirectory = directory;
    }

    private void AddEntities(IEnumerable<FileEntity> entities)
    {
        Entities = new ObservableCollection<FileEntity>(entities);
        OnPropertyChanged(nameof(Entities));
    }
    
}