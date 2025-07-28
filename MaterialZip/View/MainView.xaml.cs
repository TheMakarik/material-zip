using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MaterialZip.Convertors;
using MaterialZip.Model.Entities;
using MaterialZip.ViewModel;
using Serilog;


namespace MaterialZip.View;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView
{
    private const string WindowWasLoadedLogMessage = "Window was loaded successefully";
    private const string ExceptionOccuredLogMessage = "Exception occured";
    private const string DirectoryPathChangedUsingTextBoxLogMessage = 
        "Directory path was changed using text box editing to {path}";
    private const string TryingToChangePathToUnexistingDirectoryLogMessage =
        "Trying to change path to unexisting directory {path}, directory was not changed";
    
    private readonly ILogger _logger;
    private readonly MainViewModel _viewModel;

    public MainView(MainViewModel viewModel, ILogger logger)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        _logger = logger;
        InitializeComponent();
        _logger.Debug(WindowWasLoadedLogMessage);
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
    private async void InvokeResetDirectoryContent(object sender, MouseButtonEventArgs eventArgs)
    {
        try
        {
            var row = sender as DataGridRow;
            if (row is null)
                return;

            var fileDataGridEntity = row.DataContext as FileDataGridEntity?;

            var fileEntityObject = FileDataGridEntityToFileEntityConvertor
                .Instance
                .Convert(fileDataGridEntity, typeof(FileEntity), null, null);
            if (fileEntityObject is not FileEntity entity)
                return;
            await _viewModel.ResetDirectoryContentCommand.ExecuteAsync(entity)!;
        }
        catch (Exception exception)
        {
            _logger.Warning(exception, ExceptionOccuredLogMessage);
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
                _logger.Information(DirectoryPathChangedUsingTextBoxLogMessage, directory.Path);
            }
            else
                _logger.Warning(TryingToChangePathToUnexistingDirectoryLogMessage, directory.Path);
        }
        catch (Exception exception)
        {
            _logger.Warning(exception, ExceptionOccuredLogMessage);
        }
    }
    
}