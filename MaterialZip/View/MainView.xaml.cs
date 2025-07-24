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
    
    private readonly ILogger _logger;

    public MainView(MainViewModel viewModel, ILogger logger)
    {
        DataContext = viewModel;
        _logger = logger;
        InitializeComponent();
    }

   
    private void Close(object sender, RoutedEventArgs e)
    {
        Close(); 
    }


    private void Maximize(object sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Maximized 
            ? WindowState.Normal 
            : WindowState.Maximized;
    }

    private void Minimize(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    // WARNING: this is a HUGE crutch
    private async void InvokeResetDirectoryContent(object sender, MouseButtonEventArgs e)
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
                await (DataContext as MainViewModel)?.ResetDirectoryContentCommand.ExecuteAsync(entity)!;
                    
        }
        catch (Exception ex)
        {
           _logger.Warning(ex, ExceptionOccuredLogMessage);
        }
    }

    
}