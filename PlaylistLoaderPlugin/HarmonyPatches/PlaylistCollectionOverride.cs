using System;
using HarmonyLib;
using PlaylistLoaderLite.UI;

/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace PlaylistLoaderLite.HarmonyPatches
{
    /// <summary>
    /// This is a patch of the method <see cref="PlaylistsViewController.SetData(IAnnotatedBeatmapLevelCollection[], int, bool)"/>
    /// TODO: Remove this or replace it with your own.
    /// </summary>
    [HarmonyPatch(typeof(AnnotatedBeatmapLevelCollectionsViewController), "SetData",
        new Type[] { // Specify the types of SetDataFromLevelAsync's parameters here.
        typeof(IAnnotatedBeatmapLevelCollection[]), typeof(int), typeof(bool)})]
    class PlaylistCollectionOverride
    {
        private static IAnnotatedBeatmapLevelCollection[] loadedPlaylists;
        /// <summary>
        /// Adds this plugin's name to the beginning of the author text in the song list view.
        /// </summary>
        static void Prefix(ref IAnnotatedBeatmapLevelCollection[] annotatedBeatmapLevelCollections)
        {
            // Checks if this is the playlists view
            if (annotatedBeatmapLevelCollections[0].GetType().Equals(typeof(UserFavoritesPlaylistSO)))
            {
                IAnnotatedBeatmapLevelCollection[] tempplaylists = new IAnnotatedBeatmapLevelCollection[annotatedBeatmapLevelCollections.Length + loadedPlaylists.Length];
                for (int i = 0; i < annotatedBeatmapLevelCollections.Length; i++)
                {
                    tempplaylists[i] = annotatedBeatmapLevelCollections[i];
                }
                int j = 0;
                for (int i = annotatedBeatmapLevelCollections.Length; i < tempplaylists.Length; i++)
                {
                    tempplaylists[i] = loadedPlaylists[j++];
                }
                annotatedBeatmapLevelCollections = tempplaylists;
            }
        }

        public static int refreshPlaylists()
        {
            loadedPlaylists = LoadPlaylistScript.load();
            return loadedPlaylists.Length;
        }
    }
}
