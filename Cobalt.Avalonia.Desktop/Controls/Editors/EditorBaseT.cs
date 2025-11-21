#pragma warning disable AVP1002
using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Cobalt.Avalonia.Desktop.Core.ValueConverters;
using FluentAvalonia.Core;

namespace Cobalt.Avalonia.Desktop.Controls.Editors;

public abstract class EditorBase<T> : EditorBase
{
    private bool _hasTextChanged;
    
    static EditorBase()
    {
        TextProperty.Changed.AddClassHandler<EditorBase<T>>(OnTextChanged);
        ValueProperty.Changed.AddClassHandler<EditorBase<T>>(OnValueChanged);
        DataContextProperty.Changed.AddClassHandler<EditorBase<T>>(OnDataContextChanged);
    }

    public static readonly StyledProperty<T?> ValueProperty =
        AvaloniaProperty.Register<EditorBase<T>, T?>(nameof(Value), defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly StyledProperty<IValueConverter<T>?> ValueConverterProperty =
        AvaloniaProperty.Register<EditorBase<T>, IValueConverter<T>?>(nameof(ValueConverter));
    
    public static readonly StyledProperty<string?> ValueFormatProperty =
        AvaloniaProperty.Register<EditorBase<T>, string?>(nameof(ValueFormat));

    public static readonly StyledProperty<bool> IsNullableBoundValueProperty =
        AvaloniaProperty.Register<EditorBase<T>, bool>(nameof(IsNullableBoundValue));

    public static readonly StyledProperty<string?> ValidationPropertyNameProperty =
        AvaloniaProperty.Register<EditorBase<T>, string?>(nameof(ValidationPropertyName));    
    
    public T? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public IValueConverter<T>? ValueConverter
    {
        get => GetValue(ValueConverterProperty);
        set => SetValue(ValueConverterProperty, value);
    }
    
    public string? ValueFormat
    {
        get => GetValue(ValueFormatProperty);
        set => SetValue(ValueFormatProperty, value);
    }
    
    public bool IsNullableBoundValue
    {
        get => GetValue(IsNullableBoundValueProperty);
        set => SetValue(IsNullableBoundValueProperty, value);
    }
    
    public string? ValidationPropertyName
    {
        get => GetValue(ValidationPropertyNameProperty);
        set => SetValue(ValidationPropertyNameProperty, value);
    }

    private static void OnTextChanged(EditorBase<T> sender, AvaloniaPropertyChangedEventArgs e)
    {
        sender._hasTextChanged = true;
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        if (_hasTextChanged)
            UpdateValue();
        _hasTextChanged = false;
    }

    private static void OnValueChanged(EditorBase<T> sender, AvaloniaPropertyChangedEventArgs e)
    {
        sender.UpdateText();
    }
    
    private void UpdateText()
    {
        var formattedValue = ValueConverter?.FormatValue(Value, ValueFormat);
        SetCurrentValue(TextProperty, formattedValue); 
    }

    private void UpdateValue()
    {
        if (IsNullableBoundValue && string.IsNullOrEmpty(Text))
        {
            SetCurrentValue(ValueProperty, default);
            return;
        }
        
        // Attempt to parse the text using the value parser
        var (success, value) = ValueConverter?.TryParseValue(Text) ?? (false, default);
        if (success)
        {
            SetCurrentValue(ValueProperty, value);
            ClearValidationError();
            
            CheckForExistingValidationErrors();
        }
        else
            AddValidationError($"Value '{Text}' could not be converted.");
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (string.IsNullOrEmpty(Text) && Value is not null)
            UpdateText();
        if (Value is null && !string.IsNullOrEmpty(Text))
            UpdateValue();
    }

    private static void OnDataContextChanged(EditorBase<T> sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.OldValue is INotifyDataErrorInfo oldErrorInfo)
            oldErrorInfo.ErrorsChanged -= sender.OnViewModelErrorsChanged;
        if (e.NewValue is INotifyDataErrorInfo newErrorInfo)
        {
            newErrorInfo.ErrorsChanged += sender.OnViewModelErrorsChanged;
            sender.CheckForExistingValidationErrors();
        }
    }

    private void CheckForExistingValidationErrors()
    {
        if (!string.IsNullOrEmpty(ValidationPropertyName) && DataContext is INotifyDataErrorInfo errorInfo)
        {
            var errors = errorInfo.GetErrors(ValidationPropertyName);
            if (errors.Count() > 0)
            {
                var errorMessage = errors.ElementAt(0).ToString();
                if (!string.IsNullOrEmpty(errorMessage))
                    AddValidationError(errorMessage);
            }
        }
    }

    private void OnViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        // Check if the error is for our bound property
        if (sender is INotifyDataErrorInfo errorInfo && 
            !string.IsNullOrEmpty(ValidationPropertyName) &&
            string.Equals(e.PropertyName, ValidationPropertyName, StringComparison.Ordinal))
        {
            var errors = errorInfo.GetErrors(ValidationPropertyName);
    
            if (errors.Count() > 0)
            {
                var errorMessage = errors.ElementAt(0).ToString();
                if (!string.IsNullOrEmpty(errorMessage))
                    AddValidationError(errorMessage);
            }
            else
            {
                ClearValidationError();
            }
        }
    }

    protected void AddValidationError(string errorMessage)
    {
        DataValidationErrors.SetError(this, new DataValidationException(errorMessage));
    }

    protected void ClearValidationError()
    {
        DataValidationErrors.ClearErrors(this);
    }
}