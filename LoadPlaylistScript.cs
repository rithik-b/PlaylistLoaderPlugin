using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using SongCore;
using System.Collections.Generic;
using System.Linq;

namespace PlaylistLoaderPlugin
{
    public class LoadPlaylistScript
    {
        public static CustomPlaylistSO[] load()
        {
            string[] playlistPaths = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Playlists"), "*.json");
            CustomPlaylistSO[] playlists = new CustomPlaylistSO[playlistPaths.Length];
            for (int i = 0; i < playlists.Length; i++)
            {
                JObject playlistJSON = JObject.Parse(File.ReadAllText(playlistPaths[i]));
                JArray songs = (JArray)playlistJSON["songs"];
                List<IPreviewBeatmapLevel> beatmapLevels = new List<IPreviewBeatmapLevel>();
                for (int j = 0; j < songs.Count; j++)
                {
                    beatmapLevels.Add((IPreviewBeatmapLevel) MatchSong((string)songs[j]["hash"]));
                }
                foreach(String key in Loader.CustomLevels.Keys)
                {
                    Logger.log.Critical(Loader.CustomLevels[key].levelID.Split('_')[2]+ " " + Loader.CustomLevels[key].songName);
                }
                CustomBeatmapLevelCollectionSO customBeatmapLevelCollection = new CustomBeatmapLevelCollectionSO(beatmapLevels.ToArray());
                playlists[i] = new CustomPlaylistSO((string)playlistJSON["playlistTitle"], (string)playlistJSON["image"], customBeatmapLevelCollection);
            }
            return playlists;
        }

        private static CustomPreviewBeatmapLevel MatchSong(String hash)
        {
            if (!SongCore.Loader.AreSongsLoaded || SongCore.Loader.AreSongsLoading)
            {
                Logger.log.Info("Songs not loaded. Not Matching songs for playlist.");
                return null;
            }
            CustomPreviewBeatmapLevel x = null;
            try
            {
                if (!string.IsNullOrEmpty(hash))
                x = SongCore.Loader.CustomLevels.Values.FirstOrDefault(y => string.Equals(y.levelID.Split('_')[2], hash, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            { 
                Logger.log.Warn($"Unable to match song with {(string.IsNullOrEmpty(hash) ? " unknown hash!" : ("hash " + hash + " !"))}");
            }
            return x;
        }
    }
}