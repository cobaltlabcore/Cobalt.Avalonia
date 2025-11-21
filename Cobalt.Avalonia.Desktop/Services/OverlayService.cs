using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Cobalt.Avalonia.Desktop.Controls;

namespace Cobalt.Avalonia.Desktop.Services;

public class OverlayService : IOverlayService
{
    private ContentControl? _host;
    
    public void SetOverlayHost(ContentControl host)
        => _host = host;

    public async Task ShowAsync(Control content)
    {
        if (_host is null)
            throw new InvalidOperationException(
                "The overlay host has not been set. Use SetOverlayHost() before calling ShowAsync().");

        await HideAsync();

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            var presenter = new OverlayPresenter
            {
                Content = content
            };
            _host.Content = presenter;
        });
    }

    public async Task HideAsync()
    {
        if (_host is null)
            return;

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (_host.Content is OverlayPresenter presenter)
                presenter.Content = null;
        
            _host.Content = null;
        });
    }
}