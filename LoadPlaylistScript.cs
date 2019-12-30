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
            string[] playlistPaths = Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "Playlists"), "*.*").Where(p => p.EndsWith(".json") || p.EndsWith(".bplist")).ToArray();
            List<CustomPlaylistSO> playlists = new List<CustomPlaylistSO>();
            for (int i = 0; i < playlistPaths.Length; i++)
            {
                JObject playlistJSON = JObject.Parse(File.ReadAllText(playlistPaths[i]));
                if (playlistJSON["songs"]!=null)
                {
                    JArray songs = (JArray)playlistJSON["songs"];
                    List<IPreviewBeatmapLevel> beatmapLevels = new List<IPreviewBeatmapLevel>();
                    for (int j = 0; j < songs.Count; j++)
                    {
                        IPreviewBeatmapLevel beatmapLevel = null;
                        String hash = (string)songs[j]["hash"];
                        beatmapLevel = MatchSong(hash);
                        if(beatmapLevel!=null)
                            beatmapLevels.Add(beatmapLevel);
                        else
                        {
                            String levelID = (string)songs[j]["levelId"];
                            if (!string.IsNullOrEmpty(levelID))
                            {
                                hash = Collections.hashForLevelID(levelID);
                                beatmapLevel = MatchSong(hash);
                                if (beatmapLevel != null)
                                    beatmapLevels.Add(beatmapLevel);
                                else
                                    Logger.log.Warn($"Song not downloaded, : {(string.IsNullOrEmpty(hash) ? " unknown hash!" : ("hash " + hash + "!"))}");
                            }
                            else
                                Logger.log.Warn($"Song not downloaded, : {(string.IsNullOrEmpty(hash) ? " unknown hash!" : ("hash " + hash + "!"))}");
                        }
                    }
                    CustomBeatmapLevelCollectionSO customBeatmapLevelCollection = CustomBeatmapLevelCollectionSO.CreateInstance(beatmapLevels.ToArray());
                    String playlistTitle = "Untitled Playlist";
                    String playlistImage = CustomPlaylistSO.DEFAULT_IMAGE;
                    if ((string)playlistJSON["playlistTitle"]!=null)
                        playlistTitle = (string)playlistJSON["playlistTitle"];
                    if ((string)playlistJSON["image"] != null)
                        playlistImage = (string)playlistJSON["image"];
                    playlists.Add(CustomPlaylistSO.CreateInstance(playlistTitle, playlistImage, customBeatmapLevelCollection));
                }
            }
            return playlists.ToArray();
        }

        private static IPreviewBeatmapLevel MatchSong(String hash)
        {
            if (!SongCore.Loader.AreSongsLoaded || SongCore.Loader.AreSongsLoading)
            {
                Logger.log.Info("Songs not loaded. Not Matching songs for playlist.");
                return null;
            }
            IPreviewBeatmapLevel x = null;
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
