namespace Cobalt.Avalonia.Desktop.Core;

/// <summary>
/// Defines a contract for tracking and reporting progress information.
/// </summary>
/// <remarks>
/// This interface provides a standardized way to communicate progress updates
/// including numeric progress values, descriptive messages, and indeterminate states.
/// It's commonly used for long-running operations, file transfers, and background tasks.
/// </remarks>
public interface IProgress
{
    /// <summary>
    /// Gets or sets the current progress value as a percentage (0.0 to 100.0).
    /// </summary>
    /// <value>
    /// A double value representing the progress percentage. Typically ranges from 0.0 (no progress)
    /// to 100.0 (complete), but the exact range depends on the implementation.
    /// </value>
    double Value { get; set; }
    
    /// <summary>
    /// Gets or sets an optional descriptive message about the current progress state.
    /// </summary>
    /// <value>
    /// A string describing what operation is currently being performed, or null if no message is available.
    /// Examples might include "Loading files...", "Processing data...", or "Almost finished...".
    /// </value>
    string? Message { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the progress is indeterminate (unknown duration).
    /// </summary>
    /// <value>
    /// True if the progress duration cannot be determined (showing a spinner or pulsing bar);
    /// false if the progress can be measured with a specific value.
    /// </value>
    bool IsIndeterminate { get; set; }
    
    /// <summary>
    /// Updates one or more progress properties atomically.
    /// </summary>
    /// <param name="value">The new progress value, or null to leave unchanged.</param>
    /// <param name="message">The new progress message, or null to leave unchanged.</param>
    /// <param name="isIndeterminate">The new indeterminate state, or null to leave unchanged.</param>
    /// <remarks>
    /// This method provides a convenient way to update multiple progress properties
    /// in a single call, which can be more efficient than setting properties individually
    /// when multiple changes need to be made simultaneously.
    /// </remarks>
    void UpdateProgress(double? value = null, string? message = null, bool? isIndeterminate = null);
}