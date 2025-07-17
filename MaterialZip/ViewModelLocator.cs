using System.Collections.Concurrent;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialZip;

public class ViewLocator
{
    private const string ViewPattern = "View";
    private const string ViewModelPattern = "ViewModel";
    private const string ViewModelIsInCacheLogMessage = "ViewModel for {viewName} was found in cache";
    private const string ViewModelWasAddedToCache = "ViewModel {viewModelName} was added to the cache";
    
    private readonly ConcurrentDictionary<Type, Type> _viewModelTypeCache = new();
    private readonly ILogger _logger;

    public ViewLocator(ILogger logger)
    {
        _logger = logger;
    }
    
    public void ResolveViewModel(FrameworkElement view)
    {
        var viewType = view.GetType();
        
        if (IsViewModelInCache(viewType, out var viewModelType))
            _logger.Debug(ViewModelIsInCacheLogMessage, viewType.FullName);
        else
        {
            viewModelType = GetViewModelType(viewType);
            if (viewModelType == null) return;
            
            _viewModelTypeCache.TryAdd(viewType, viewModelType);
            _logger.Debug(ViewModelWasAddedToCache, viewModelType.FullName);
        }
        
        view.DataContext = Ioc.Default.GetRequiredService(viewModelType);
    }
    
    private Type? GetViewModelType(Type viewType)
    {
        var viewModelTypeName = viewType.FullName?
            .Replace(ViewPattern, ViewModelPattern);
        
        if (string.IsNullOrEmpty(viewModelTypeName))
            return null;
        
        return Type.GetType(viewModelTypeName);
    }
    
    private bool IsViewModelInCache(Type viewType, out Type? viewModelType)
        => _viewModelTypeCache.TryGetValue(viewType, out viewModelType);
}