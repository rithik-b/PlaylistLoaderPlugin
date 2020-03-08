using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine;
using SongCore;
using System.Collections;
using PlaylistLoaderLite.HarmonyPatches;

namespace PlaylistLoaderLite.UI
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