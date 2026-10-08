using Repilot.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Repilot.Pages;

public sealed partial class AboutPage : Page
{
    private UpdateService.UpdateCheckResult? _update;

    public AboutPage()
    {
        InitializeComponent();

        var v = typeof(AboutPage).Assembly.GetName().Version!;
        VersionText.Text = $"Version {v.Major}.{v.Minor}.{v.Build}";
    }

    /// <summary>Checks GitHub releases for an unpackaged development build.</summary>
    private async void CheckForUpdates_Click(object sender, RoutedEventArgs e)
    {
        if (UpdateService.IsPackaged)
        {
            UpdateStatusText.Text = UpdateService.OpenStoreUpdates()
                ? "The Microsoft Store is open to Updates & downloads. Check there for available updates."
                : "Unable to open the Microsoft Store.";
            return;
        }

        CheckUpdateButton.IsEnabled = false;
        CheckUpdateButton.Content = "Checking...";
        UpdateStatusText.Text = "Checking for updates...";

        var result = await UpdateService.CheckForUpdateAsync();
        if (result == null)
        {
            UpdateStatusText.Text = "Unable to check for updates. Check your internet connection.";
        }
        else if (result.UpdateAvailable)
        {
            _update = result;
            UpdateStatusText.Text = $"Version {result.LatestVersion} is available (you have {result.CurrentVersion}).";
            CheckUpdateButton.Content = "View Release";
            CheckUpdateButton.Click -= CheckForUpdates_Click;
            CheckUpdateButton.Click += ViewRelease_Click;
            CheckUpdateButton.IsEnabled = true;
            return;
        }
        else
        {
            UpdateStatusText.Text = $"You're up to date ({result.CurrentVersion}).";
        }

        CheckUpdateButton.Content = "Check for Updates";
        CheckUpdateButton.IsEnabled = true;
    }

    private void ViewRelease_Click(object sender, RoutedEventArgs e)
    {
        if (_update?.ReleaseUrl is { Length: > 0 } url)
            UpdateService.OpenUrl(url);
    }
}
