using BeatSaberMarkupLanguage.MenuButtons;
using PlaylistLoaderPlugin.HarmonyPatches;
using UnityEngine;
using SongCore;
using System.Collections;

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
            StartCoroutine(RefreshButtonFlow());
        }

        internal IEnumerator RefreshButtonFlow()
        {
            Loader.Instance.RefreshSongs();
            yield return new WaitUntil(() => Loader.AreSongsLoaded == true);
            PlaylistCollectionOverride.refreshPlaylists();
        }
    }
}