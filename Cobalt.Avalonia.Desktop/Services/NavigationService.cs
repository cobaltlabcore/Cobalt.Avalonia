using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace Cobalt.Avalonia.Desktop.Services;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    private NavigationView? _navigationView;
    private Frame? _frame;
    private Type? _currentPageType;

    private NavigationView NavigationView
        => _navigationView ?? throw new InvalidOperationException("NavigationView is not set. Use Initialize first.");
    
    private Frame Frame
        => _frame ?? throw new InvalidOperationException("Frame is not set. Use Initialize first.");
    
    public object? CurrentPage { get; private set; }
    public ObservableCollection<NavigationViewItem> MenuItems { get; } = [];
    public ObservableCollection<NavigationViewItem> FooterMenuItems { get; } = [];

    public void Initialize(NavigationView navigationView, Frame frame)
    {
        _navigationView = navigationView;
        _frame = frame;
        _frame.NavigationPageFactory = new NavigationPageFactory(serviceProvider);
        
        RegisterNavigationEvents(navigationView, frame);
    }

    public void NavigateTo(Type pageType)
    {
        Func<NavigationViewItem, bool> predicate = x => x.Tag is Type type && type == pageType;
        if (MenuItems.Any(predicate))
            MenuItems.First(predicate).IsSelected = true;
        else if (FooterMenuItems.Any(predicate))
            FooterMenuItems.First(predicate).IsSelected = true;
        else
        {
            NavigationView.SelectedItem = null;
            Frame.Navigate(pageType);
        }
    }

    public void RegisterNavigationEvents(NavigationView navigationView, Frame frame)
    {
        NavigationView.SelectionChanged += NavigationViewSelectionChanged;
        Frame.Navigating += OnNavigating;
        Frame.Navigated += OnNavigated;
    }

    private void NavigationViewSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is NavigationViewItem { Tag: Type pageType })
            Frame.Navigate(pageType);
    }

    private void OnNavigating(object sender, NavigatingCancelEventArgs e)
    {
        if (CurrentPage is INavigationPage page)
            page.OnNavigatedFrom();
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        CurrentPage = e.Content;
        _currentPageType = e.Content?.GetType();
        
        if (CurrentPage is INavigationPage page)
            page.OnNavigatingTo();
    }
}

public class NavigationPageFactory(IServiceProvider serviceProvider) : INavigationPageFactory
{
    public Control GetPage(Type srcType)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, srcType) as Control
               ?? throw new InvalidOperationException($"Could not create page type {srcType.FullName}");
    }

    public Control GetPageFromObject(object target)
    {
        throw new NotImplementedException();
    }
}