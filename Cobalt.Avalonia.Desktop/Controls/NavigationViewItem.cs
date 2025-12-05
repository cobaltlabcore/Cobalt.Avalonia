using System;
using Avalonia;
using Avalonia.Controls;

namespace Cobalt.Avalonia.Desktop.Controls;

public class NavigationViewItem : ListBoxItem
{
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<NavigationViewItem, object?>(nameof(Icon));

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(NavigationViewItem);
}