using System;
using Harmony;
/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace PlaylistLoaderPlugin.HarmonyPatches
{
    /// <summary>
    /// This is a patch of the method <see cref="PlaylistsViewController.SetData(IAnnotatedBeatmapLevelCollection[], int, bool)"/>
    /// TODO: Remove this or replace it with your own.
    /// </summary>
    [HarmonyPatch(typeof(PlaylistsViewController), "SetData",
        new Type[] { // Specify the types of SetDataFromLevelAsync's parameters here.
        typeof(IAnnotatedBeatmapLevelCollection[]), typeof(int), typeof(bool)})]
    class PlaylistCollectionOverride
    {
        private static IAnnotatedBeatmapLevelCollection[] loadedPlaylists;
        /// <summary>
        /// Adds this plugin's name to the beginning of the author text in the song list view.
        /// </summary>
        static void Prefix(ref IAnnotatedBeatmapLevelCollection[] playlists)
        {
            if(playlists[0].GetType().Equals(typeof(UserFavoritesPlaylistSO))) //Checks if this is the playlists view
            {
                if(loadedPlaylists==null)
                    loadedPlaylists = LoadPlaylistScript.load();        
                IAnnotatedBeatmapLevelCollection[] tempplaylists = new IAnnotatedBeatmapLevelCollection[playlists.Length + loadedPlaylists.Length];
                for (int i = 0; i < playlists.Length; i++)
                {
                    tempplaylists[i] = playlists[i];
                }
                int j = 0;
                for (int i = playlists.Length; i < tempplaylists.Length; i++)
                {
                    tempplaylists[i] = loadedPlaylists[j++];
                }
                playlists = tempplaylists;
            }
        }
    }
}
