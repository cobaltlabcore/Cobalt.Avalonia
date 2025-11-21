namespace Cobalt.Avalonia.Desktop.Core.Bootstrapper;

/// <summary>
/// Represents the current operational state of a bootstrapper during its lifecycle.
/// </summary>
/// <remarks>
/// This enum defines the possible states that a bootstrapper can be in during its execution:
/// <list type="bullet">
/// <item><description><see cref="NotStarted"/> - Initial state before any operations begin</description></item>
/// <item><description><see cref="Starting"/> - Transitional state during initialization</description></item>
/// <item><description><see cref="Started"/> - Active operational state</description></item>
/// <item><description><see cref="Stopping"/> - Transitional state during shutdown</description></item>
/// <item><description><see cref="Stopped"/> - Final state after shutdown completion</description></item>
/// </list>
/// State transitions follow a defined pattern: NotStarted → Starting → Started → Stopping → Stopped
/// </remarks>
public enum BootstrapperState
{
    /// <summary>
    /// The bootstrapper has not been started yet and is in its initial state.
    /// This is the default state when a bootstrapper instance is first created.
    /// </summary>
    NotStarted,

    /// <summary>
    /// The bootstrapper is currently in the process of starting up.
    /// This is a transitional state during initialization, host creation, and service configuration.
    /// </summary>
    Starting,

    /// <summary>
    /// The bootstrapper has successfully started and is now running.
    /// The application host is active and all services are available for use.
    /// </summary>
    Started,

    /// <summary>
    /// The bootstrapper is currently in the process of shutting down.
    /// This is a transitional state during graceful shutdown and resource cleanup.
    /// </summary>
    Stopping,

    /// <summary>
    /// The bootstrapper has completed its shutdown process and has stopped.
    /// All resources have been disposed and the host is no longer active.
    /// </summary>
    Stopped
}