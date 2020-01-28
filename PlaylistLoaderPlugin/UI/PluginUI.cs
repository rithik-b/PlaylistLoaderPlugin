using BeatSaberMarkupLanguage.MenuButtons;
using PlaylistLoaderPlugin.HarmonyPatches;

namespace PlaylistLoaderPlugin.UI
{
    public class PluginUI : PersistentSingleton<PluginUI>
    {
        public MenuButton refreshButton;

        internal void Setup()
        {
            refreshButton = new MenuButton("Refresh Playlists", "Refresh Songs & Playlists", RefreshButtonPressed, true);
            MenuButtons.instance.RegisterButton(refreshButton);
        }

        internal void RefreshButtonPressed()
        {
            RefreshButtonFlow();
        }

        internal void RefreshButtonFlow()
        {
            SongCore.Loader.Instance.RefreshSongs();
            PlaylistCollectionOverride.refreshPlaylists();
        }
    }
}