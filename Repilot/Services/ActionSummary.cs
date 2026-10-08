using Repilot.Models;

namespace Repilot.Services;

/// <summary>Human-readable one-line description of a configured action (for the settings UI).</summary>
public static class ActionSummary
{
    public static string Describe(CopilotActionData a) => a.Type switch
    {
        CopilotActionType.None => "Do nothing",
        CopilotActionType.Text => string.IsNullOrEmpty(a.Text)
            ? "Type text (not set)"
            : $"Type text: {a.Text}",
        CopilotActionType.KeyCombo => a.Combo is { IsEmpty: false }
            ? $"Send keys: {a.Combo}"
            : "Send a key combination (not set)",
        CopilotActionType.LaunchApp => string.IsNullOrWhiteSpace(a.LaunchPath)
            ? "Open an app (not set)"
            : $"Open: {(string.IsNullOrWhiteSpace(a.LaunchDisplayName) ? a.LaunchPath : a.LaunchDisplayName)}",
        CopilotActionType.WindowsFunction => string.IsNullOrWhiteSpace(a.WindowsFunctionId)
            ? "Run a Windows function (not set)"
            : $"Windows function: {WindowsFunctionCatalog.NameFor(a.WindowsFunctionId)}",
        _ => "Unknown",
    };
}
