using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialZip.Model.Entities;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Serilog;

namespace MaterialZip.ViewModel;

public partial class MainViewModel : ViewModelBase
{
    private const string DefaultLogicalDrivesPath = "/";
    
    private readonly IExplorer _explorer;
    private readonly IExplorerHistory _history;
    private readonly ILastDirectoryBuffer _buffer;
    private readonly IHoverButtonHexGetter _hoverButtonHexGetter;
    private readonly IGitHubSourceOpener _gitHubSourceOpener;
    
    [ObservableProperty] private string _currentPath;
    [ObservableProperty] private ObservableCollection<FileEntity> _selectedEntities = new();
    [ObservableProperty] private ObservableCollection<FileEntity> _entities = new();
    [ObservableProperty] private bool _canUndo;
    [ObservableProperty] private bool _canRedo;
    
    public string HoverButtonHex { get; init; } 
    
    public MainViewModel(
        ILastDirectoryBuffer buffer,
        IExplorer explorer,
        IExplorerHistory history,
        IHoverButtonHexGetter hoverButtonHexGetter,
        IGitHubSourceOpener gitHubSourceOpener
        )
    {
        _buffer = buffer;
        _explorer = explorer;
        _history = history;
        _hoverButtonHexGetter = hoverButtonHexGetter;
        _gitHubSourceOpener = gitHubSourceOpener;

        HoverButtonHex = _hoverButtonHexGetter.GetHoverButtonHex();
        var directory = _buffer.FromBuffer();
        ResetDirectoryContent(directory);
    }

    

    [RelayCommand]
    private async Task ResetDirectoryContent(FileEntity directory)
    {
        await ResetDirectory(directory, updateHistory: true);
    }

    [RelayCommand]
    private void OpenGitHubSource()
    {
        _gitHubSourceOpener.TryOpen();
    }

    [RelayCommand]
    private  async Task Redo()
    {
        if (!_history.CanRedo)
            return;
        
        _history.Redo();
        await ResetDirectoryContentToCurrentDirectory();
    }

    [RelayCommand]
    private async Task Undo()
    {
        if (!_history.CanUndo)
            return;
        
        _history.Undo();
        await ResetDirectoryContentToCurrentDirectory();
    }
    
    private void SaveDirectory(FileEntity directory, bool updateHistory)
    {
        _buffer.ToBuffer(directory);
        
        if(updateHistory)
             _history.CurrentDirectory = directory;
        
        CurrentPath = directory.Path;
        UpdateCanRedoAndCanUndo();
    }

    private void UpdateCanRedoAndCanUndo()
    {
        CanRedo = _history.CanRedo;
        CanUndo = _history.CanUndo;
    }

    private async Task ResetDirectory(FileEntity directory, bool updateHistory)
    {
        if (!directory.IsDirectory)
            return;
        
        if (directory.Path == DefaultLogicalDrivesPath)
            AddEntities(await _explorer.GetLogicalDrivesAsync());
        else
            AddEntities(await _explorer.GetDirectoryContentAsync(directory));
        
        SaveDirectory(directory, updateHistory);
    }

    private void AddEntities(IEnumerable<FileEntity> entities)
    {
        Entities = new ObservableCollection<FileEntity>(entities);
    }

    private async Task ResetDirectoryContentToCurrentDirectory() 
        => await ResetDirectory(_history.CurrentDirectory, updateHistory: false);


}