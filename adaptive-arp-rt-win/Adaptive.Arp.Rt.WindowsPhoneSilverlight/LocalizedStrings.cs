using Adaptive.Arp.Rt.WindowsPhoneSilverlight.Resources;

namespace Adaptive.Arp.Rt.WindowsPhoneSilverlight
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}