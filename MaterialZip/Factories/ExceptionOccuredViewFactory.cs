
using MaterialZip.Factories.Abstractions;
using MaterialZip.View;
using MaterialZip.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MaterialZip.Factories;

public class ExceptionOccuredViewFactory(IServiceProvider services, ILogger<ExceptionOccuredViewFactory> logger) : IExceptionOccuredViewFactory
{
    private const string ExceptionViewShowing = "Exception view is showing";
    
    public void Show(Exception exception)
    {
        var window = services.GetRequiredService<ExceptionOccuredView>();
        var viewModel = services.GetRequiredService<ExceptionOccuredViewModel>();
        window.DataContext = viewModel;
        viewModel.ExceptionString = exception.ToString();
        logger.LogDebug(ExceptionViewShowing);
        window.ShowDialog();
    }
}