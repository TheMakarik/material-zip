using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialZip.Convertors;
using MaterialZip.Model.Entities;
using MaterialZip.ViewModel;


namespace MaterialZip.View;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView
{

    public MainView(MainViewModel viewModel)
    {
        this.DataContext = viewModel;
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
            Console.WriteLine(ex.Source + ex.StackTrace);
        }
    }

   
}