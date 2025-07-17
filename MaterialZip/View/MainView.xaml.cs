using System.Windows;

namespace MaterialZip.View;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : Window
{

    public MainView(ViewModelLocator locator)
    {
        locator.ResolveViewModel(this);
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
}