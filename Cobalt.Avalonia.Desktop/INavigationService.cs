using System;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;

namespace Cobalt.Avalonia.Desktop;

public interface INavigationService
{
    ObservableCollection<NavigationViewItem> MenuItems { get; }
    ObservableCollection<NavigationViewItem> FooterMenuItems { get; }
    void Initialize(NavigationView navigationView, Frame frame);
    void NavigateTo(Type pageType);
}