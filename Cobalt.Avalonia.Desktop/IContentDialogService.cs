using System.Threading.Tasks;

namespace Cobalt.Avalonia.Desktop;

public enum ContentDialogResult
{
    Ok,
    Cancel,
    Yes,
    No
}

public enum ContentDialogButtons
{
    Ok,
    OkCancel,
    YesNo,
    YesNoCancel
}

public interface IContentDialogService
{
    void SetTexts(string okText, string cancelText, string yesText, string noText);
    Task<ContentDialogResult> ShowAsync(string title, object content, ContentDialogButtons buttons);
}