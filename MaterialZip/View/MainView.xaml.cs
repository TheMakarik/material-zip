using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialZip.Convertors;
using MaterialZip.Model.Entities;
using MaterialZip.ViewModel;
using Microsoft.Extensions.Logging;


namespace MaterialZip.View;

public sealed partial class MainView
{
    private const string WindowWasLoadedLogMessage = "Window was loaded successefully";
    private const string ExceptionOccuredLogMessage = "Exception occured";
    private const string DirectoryPathChangedUsingTextBoxLogMessage = 
        "Directory path was changed using text box editing to {path}";
    private const string TryingToChangePathToUnexistingDirectoryLogMessage =
        "Trying to change path to unexisting directory {path}, directory was not changed";
   
    private readonly ILogger<MainView> _logger;
    private readonly MainViewModel _viewModel;
    private string A = "AAAAA";

    public MainView(MainViewModel viewModel, ILogger<MainView> logger)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        _logger = logger;
        InitializeComponent();
        _logger.LogDebug(WindowWasLoadedLogMessage);
      
    }

   
    private void Close(object sender, RoutedEventArgs e)
    {
        Close(); 
    }


    private void Maximize(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized 
            ? WindowState.Normal 
            : WindowState.Maximized;
    }

    private void Minimize(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    // WARNING: this is a HUGE crutch
    private async void InvokeResetDirectoryContent(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        try
        {
            if (mouseButtonEventArgs.ChangedButton != MouseButton.Left)
                return;
            
            var entity = GetFileEntity(sender);
            if (entity is null)
                return;
            
            await _viewModel.ResetDirectoryContentCommand.ExecuteAsync(entity);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, ExceptionOccuredLogMessage);
        }
    }


    private async void PathTextBox_OnKeyDown(object sender, KeyEventArgs eventArgs)
    {
        try
        {
            var directory = new FileEntity( PathTextBox.Text, IsDirectory: true);
        
            if (eventArgs.Key != Key.Enter)
                return;
        
            if (Directory.Exists(directory.Path))
            {
                await _viewModel.ResetDirectoryContentCommand.ExecuteAsync(directory);
                _logger.LogInformation(DirectoryPathChangedUsingTextBoxLogMessage, directory.Path);
            }
            else
                _logger.LogWarning(TryingToChangePathToUnexistingDirectoryLogMessage, directory.Path);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, ExceptionOccuredLogMessage);
        }
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
        base.OnMouseLeftButtonDown(e);
    }

  

    private FileEntity? GetFileEntity(object rowObject)
    {
        var row = rowObject as DataGridRow;
        var fileDataGridEntity = row?.DataContext as FileDataGridEntity?;
        var fileEntityObject = FileDataGridEntityToFileEntityConvertor
            .Instance
            .Convert(fileDataGridEntity, typeof(FileEntity), null, null);
        
        return fileEntityObject as FileEntity?;
    }
    
    
}