using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace Cobalt.Avalonia.Desktop.Controls.Editors;

public abstract class EditorBase : TextBox
{
    public EditorBase()
    {
        Buttons = [];
    }
    
    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<EditorBase, string?>(name: nameof(Title), defaultValue: null);

    public static readonly StyledProperty<List<Button>?> ButtonsProperty =
        AvaloniaProperty.Register<EditorBase, List<Button>?>(nameof(Buttons), defaultValue: null);

    public static readonly StyledProperty<double> TextEditorMinHeightProperty =
        AvaloniaProperty.Register<EditorBase, double>(nameof(TextEditorMinHeight));

    public static readonly StyledProperty<double> TextEditorMaxHeightProperty =
        AvaloniaProperty.Register<EditorBase, double>(nameof(TextEditorMaxHeight), defaultValue: double.PositiveInfinity);
    
    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public List<Button>? Buttons
    {
        get => GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }
    
    public double TextEditorMinHeight
    {
        get => GetValue(TextEditorMinHeightProperty);
        set => SetValue(TextEditorMinHeightProperty, value);
    }
    
    public double TextEditorMaxHeight
    {
        get => GetValue(TextEditorMaxHeightProperty);
        set => SetValue(TextEditorMaxHeightProperty, value);
    }
}