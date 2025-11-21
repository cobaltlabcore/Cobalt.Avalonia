using System;
using Avalonia;
using Cobalt.Avalonia.Desktop.Core.Bootstrapper;

namespace Cobalt.Avalonia.Desktop.Bootstrapper;

// TODO: Rename to AvaloniaDesktopBootstrapper ?
public abstract class AvaloniaBootstrapper(string[] args) : BootstrapperBase
{
    protected abstract Func<Application> AppFactory { get; }
    
    protected override void InternalStart()
    {
        var builder = AppBuilder.Configure(AppFactory)
            .UsePlatformDetect()
            .WithInterFont();

        builder.StartWithClassicDesktopLifetime(args);
    }
}