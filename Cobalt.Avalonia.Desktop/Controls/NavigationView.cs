using System;
using System.Collections;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Media;

namespace Cobalt.Avalonia.Desktop.Controls;

[TemplatePart("PART_PaneListBox", typeof(ListBox))]
public class NavigationView : ContentControl
{
    public static readonly StyledProperty<IList> MenuItemsProperty =
        AvaloniaProperty.Register<NavigationView, IList>(nameof(MenuItems));

    public static readonly StyledProperty<object?> SelectedItemProperty =
        AvaloniaProperty.Register<NavigationView, object?>(nameof(SelectedItem));

    public static readonly StyledProperty<double> PaneWidthProperty =
        AvaloniaProperty.Register<NavigationView, double>(nameof(PaneWidth), 72.0);

    public static readonly StyledProperty<IBrush?> PaneBackgroundProperty =
        AvaloniaProperty.Register<NavigationView, IBrush?>(nameof(PaneBackground));

    public NavigationView()
    {
        MenuItems = new AvaloniaList<object>();
    }

    public IList MenuItems
    {
        get => GetValue(MenuItemsProperty);
        set => SetValue(MenuItemsProperty, value);
    }

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public double PaneWidth
    {
        get => GetValue(PaneWidthProperty);
        set => SetValue(PaneWidthProperty, value);
    }

    public IBrush? PaneBackground
    {
        get => GetValue(PaneBackgroundProperty);
        set => SetValue(PaneBackgroundProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(NavigationView);
}