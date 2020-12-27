using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

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
    }
}
