using BeatSaberMarkupLanguage.MenuButtons;
using PlaylistLoaderPlugin.HarmonyPatches;

namespace PlaylistLoaderPlugin.UI
{
    public class PluginUI : PersistentSingleton<PluginUI>
    {
        public MenuButton refreshButton;

        internal void Setup()
        {
            refreshButton = new MenuButton("Refresh Playlists", "Refresh Playlists (PlaylistLoaderPlugin)", RefreshButtonPressed, true);
            MenuButtons.instance.RegisterButton(refreshButton);
        }

        internal void RefreshButtonPressed()
        {
            RefreshButtonFlow();
        }

        internal void RefreshButtonFlow()
        {
            PlaylistCollectionOverride.refreshPlaylists();
        }
    }
}