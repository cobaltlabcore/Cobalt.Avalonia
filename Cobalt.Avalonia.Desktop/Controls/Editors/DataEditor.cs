using Avalonia;
using Cobalt.Avalonia.Desktop.Core.ValueConverters;

namespace Cobalt.Avalonia.Desktop.Controls.Editors;

/// <summary>
/// Specifies the encoding format for data representation.
/// </summary>
public enum DataEncoding
{
    /// <summary>
    /// Hexadecimal encoding (base 16).
    /// </summary>
    Hexadecimal,
    /// <summary>
    /// Base64 encoding.
    /// </summary>
    Base64
}

/// <summary>
/// Editor control for byte array data with support for different encoding formats.
/// Allows users to input and edit binary data as hexadecimal or Base64 text.
/// </summary>
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class DataEditor : EditorBase<byte[]?>
{
    static DataEditor()
    {
        EncodingProperty.Changed.AddClassHandler<DataEditor>(OnEncodingChanged);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataEditor"/> class.
    /// Sets the default value converter to hexadecimal format.
    /// </summary>
    public DataEditor()
    {
        ValueConverter ??= ValueConverterFactory.CreateDefaultHexadecimalValueConverter();
    }
    
    public static readonly StyledProperty<DataEncoding> EncodingProperty =
        AvaloniaProperty.Register<DataEditor, DataEncoding>(nameof(Encoding));

    public DataEncoding Encoding
    {
        get => GetValue(EncodingProperty);
        set => SetValue(EncodingProperty, value);
    }

    private static void OnEncodingChanged(DataEditor sender, AvaloniaPropertyChangedEventArgs e)
    {
        // Validate the new value is a valid DataEncoding enum value
        if (e.NewValue is not DataEncoding encoding) return;

        // Switch to the appropriate parser based on the selected encoding
        sender.ValueConverter = encoding switch
        {
            DataEncoding.Hexadecimal => ValueConverterFactory.CreateDefaultHexadecimalValueConverter(),
            DataEncoding.Base64 => ValueConverterFactory.CreateDefaultBase64ValueConverter(),
            _ => sender.ValueConverter // Keep current parser for any unexpected values
        }; 
    }
}