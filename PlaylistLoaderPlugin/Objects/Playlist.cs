using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System;

namespace PlaylistLoaderLite
{
    public class Playlist
    {
        internal CustomPlaylistSO _playlistSO;
        internal string _playlistPath;
        internal JObject _playlistJSON;

        public IBeatmapLevelCollection beatmapLevelCollection
        {
            get
            {
                return _playlistSO.beatmapLevelCollection;
            }
        }

        public string collectionName
        {
            get
            {
                return _playlistSO.collectionName;
            }
        }

        public Sprite coverImage
        {
            get
            {
                return _playlistSO.coverImage;
            }
        }

        public Playlist(CustomPlaylistSO playlistSO, string playlistPath, JObject playlistJSON)
        {
            _playlistSO = playlistSO;
            _playlistPath = playlistPath;
            _playlistJSON = playlistJSON;
        }

        public void editBeatMapLevels(IPreviewBeatmapLevel[] beatmapLevels)
        {
            string playlistTitle = _playlistSO.collectionName;
            Sprite playlistImage = _playlistSO.coverImage;
            CustomBeatmapLevelCollectionSO customBeatmapLevelCollection = CustomBeatmapLevelCollectionSO.CreateInstance(beatmapLevels);
            _playlistSO = CustomPlaylistSO.CreateInstance(playlistTitle, playlistImage, customBeatmapLevelCollection);
            dumpToJSON();
        }

        private void dumpToJSON()
        {
            JArray songsArray = new JArray();
            _playlistJSON.Remove("songs");
            foreach (IPreviewBeatmapLevel previewBeatmapLevel in beatmapLevelCollection.beatmapLevels)
            {
                JObject level = new JObject();
                if (previewBeatmapLevel.levelID.StartsWith("custom_level_"))
                {
                    level["hash"] = previewBeatmapLevel.levelID.Split('_')[2];
                    songsArray.Add(level);
                }
                else
                {
                    level["levelID"] = previewBeatmapLevel.levelID;
                    songsArray.Add(level);
                }
            }
            _playlistJSON["songs"] = songsArray;
            using (StreamWriter file = File.CreateText(_playlistPath))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                _playlistJSON.WriteTo(writer);
            }
        }

        public static void CreatePlaylist(string playlistName, string playlistAuthorName)
        {
            string playlistFolderPath = Path.Combine(Environment.CurrentDirectory, "Playlists");

            JObject playlistJSON = new JObject();
            playlistJSON["playlistTitle"] = playlistName;
            playlistJSON["playlistAuthor"] = playlistAuthorName;
            playlistJSON["image"] = CustomPlaylistSO.DEFAULT_IMAGE;
            playlistJSON["songs"] = new JArray();

            string playlistFileName = String.Join("_", playlistName.Split(' '));
            string playlistPath = Path.Combine(playlistFolderPath, playlistFileName + ".json");
            string originalPlaylistPath = Path.Combine(playlistFolderPath, playlistFileName);
            int dupNum = 0;
            while(File.Exists(playlistPath))
            {
                dupNum++;
                playlistPath = originalPlaylistPath + string.Format("({0}).json", dupNum);
            }

            using (StreamWriter file = File.CreateText(playlistPath))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                playlistJSON.WriteTo(writer);
            }
            CustomBeatmapLevelCollectionSO customBeatmapLevelCollection = CustomBeatmapLevelCollectionSO.CreateInstance(new IPreviewBeatmapLevel[0]);
            CustomPlaylistSO playlistSO = CustomPlaylistSO.CreateInstance(playlistName, CustomPlaylistSO.DEFAULT_IMAGE, customBeatmapLevelCollection);
            Playlist playlist = new Playlist(playlistSO, playlistPath, playlistJSON);
            LoadPlaylistScript.loadedPlaylists.Add(playlist);
        }
    }
}
