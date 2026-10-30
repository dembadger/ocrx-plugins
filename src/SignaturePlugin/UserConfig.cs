using Ocrx.Sdk;

namespace SignaturePlugin;

internal sealed class SignaturePluginConfig : PluginConfig
{
    public string OverlayTheme { get; set; } = OverlayThemes.Default;
    public string Position { get; set; } = OverlayPositions.TopCenter;
    public int CounterRoiWidth { get; set; } = Rois.DefaultCounterWidth;
    public int CounterRoiHorizontalOffset { get; set; } = 0;
}

internal static class UserConfig
{
    private const string ResourceName = "SignaturePlugin.config.json";

    public static string Ensure() =>
        ConfigSeed.EnsureInLocalAppData(typeof(UserConfig).Assembly, ResourceName, "SignaturePlugin");
}
