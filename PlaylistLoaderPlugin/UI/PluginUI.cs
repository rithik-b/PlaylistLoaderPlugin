using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine;
using SongCore;
using System.Collections;
using PlaylistLoaderLite.HarmonyPatches;

namespace PlaylistLoaderLite.UI
{
    public class PluginUI : PersistentSingleton<PluginUI>
    {
        public MenuButton _refreshButton;
        internal static ProgressBar _progressBar;
        const int MESSAGE_TIME = 5;

        internal void Setup()
        {
            _refreshButton = new MenuButton("Refresh Playlists", "Refresh Songs & Playlists", RefreshButtonPressed, true);
            MenuButtons.instance.RegisterButton(_refreshButton);
            LaunchLoadPlaylists();
        }

        internal void LaunchLoadPlaylists()
        {
            StartCoroutine(LaunchLoadPlaylistsFlow());
        }

        internal void RefreshButtonPressed()
        {
            StartCoroutine(RefreshButtonFlow());
        }

        internal IEnumerator LaunchLoadPlaylistsFlow()
        {
            // Wait for SongCore plugin to load
            yield return new WaitUntil(() => Loader.Instance != null);
            _progressBar = ProgressBar.Create();
            StartCoroutine(RefreshButtonFlow());
        }

        internal IEnumerator RefreshButtonFlow()
        {
            if (!Loader.AreSongsLoading)
                Loader.Instance.RefreshSongs();

            yield return new WaitUntil(() => Loader.AreSongsLoaded == true);
            int numPlaylists = PlaylistCollectionOverride.refreshPlaylists();

            _progressBar.enabled = true;
            _progressBar.ShowMessage(string.Format("\n{0} playlists loaded.", numPlaylists), MESSAGE_TIME);
        }
    }
}