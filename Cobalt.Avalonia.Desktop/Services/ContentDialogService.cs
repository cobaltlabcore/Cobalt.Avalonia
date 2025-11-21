using System;
using System.Threading.Tasks;
using FluentAvalonia.UI.Controls;

namespace Cobalt.Avalonia.Desktop.Services;

public class ContentDialogService : IContentDialogService
{
    private string _okText = "Ok";
    private string _cancelText = "Cancel";
    private string _yesText = "Yes";
    private string _noText = "No";

    public void SetTexts(string okText, string cancelText, string yesText, string noText)
        => (_okText, _cancelText, _yesText, _noText) = (okText, cancelText, yesText, noText);

    public async Task<ContentDialogResult> ShowAsync(string title, object content, ContentDialogButtons buttons)
    {
        var dialog = new ContentDialog()
        {
            Title = title,
            Content = content,
            PrimaryButtonText = GetPrimaryButtonText(buttons),
            SecondaryButtonText = GetSecondaryButtonText(buttons),
            CloseButtonText = GetNoneButtonText(buttons)
        };
        
        var result = await dialog.ShowAsync();
        
        return GetResult(result, buttons);
    }

    private ContentDialogResult GetResult(
        FluentAvalonia.UI.Controls.ContentDialogResult originalResult,
        ContentDialogButtons buttons)
    {
        if (originalResult == FluentAvalonia.UI.Controls.ContentDialogResult.Primary)
        {
            return buttons switch
            {
                ContentDialogButtons.Ok => ContentDialogResult.Ok,
                ContentDialogButtons.OkCancel => ContentDialogResult.Ok,
                ContentDialogButtons.YesNo => ContentDialogResult.Yes,
                ContentDialogButtons.YesNoCancel => ContentDialogResult.Yes,
                _ => throw new InvalidOperationException("Invalid buttons")
            };
        }
        else if (originalResult == FluentAvalonia.UI.Controls.ContentDialogResult.Secondary)
        {
            return buttons switch
            {
                ContentDialogButtons.YesNo => ContentDialogResult.No,
                ContentDialogButtons.YesNoCancel => ContentDialogResult.No,
                _ => throw new InvalidOperationException("Invalid configuration")
            };
        }
        else
        {
            return ContentDialogResult.Cancel;
        }
    }

    private string GetPrimaryButtonText(ContentDialogButtons buttons) => buttons switch
    {
        ContentDialogButtons.Ok => _okText,
        ContentDialogButtons.OkCancel => _okText,
        ContentDialogButtons.YesNo => _yesText,
        ContentDialogButtons.YesNoCancel => _yesText,
        _ => throw new InvalidOperationException("Invalid buttons")
    };

    private string? GetSecondaryButtonText(ContentDialogButtons buttons) => buttons switch
    {
        ContentDialogButtons.Ok => null,
        ContentDialogButtons.OkCancel => null,
        ContentDialogButtons.YesNo => _noText,
        ContentDialogButtons.YesNoCancel => _noText,
        _ => null
    };

    private string? GetNoneButtonText(ContentDialogButtons buttons) => buttons switch
    {
        ContentDialogButtons.Ok => null,
        ContentDialogButtons.OkCancel => _cancelText,
        ContentDialogButtons.YesNo => null,
        ContentDialogButtons.YesNoCancel => _cancelText,
        _ => throw new InvalidOperationException("Invalid buttons")
    };
}