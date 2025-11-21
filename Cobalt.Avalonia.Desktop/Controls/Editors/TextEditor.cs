using Cobalt.Avalonia.Desktop.Core.ValueConverters;

namespace Cobalt.Avalonia.Desktop.Controls.Editors;

/// <summary>
/// Editor control for string values. Provides a simple text input interface with value binding support.
/// </summary>
public class TextEditor : EditorBase<string?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextEditor"/> class.
    /// Sets the default value converter for string handling.
    /// </summary>
    public TextEditor()
    {
        ValueConverter ??= ValueConverterFactory.CreateDefaultStringValueConverter();
    }
}