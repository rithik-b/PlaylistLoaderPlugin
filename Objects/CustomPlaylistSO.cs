using System;
using Polyglot;
using UnityEngine;

public class CustomPlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
{

	public CustomPlaylistSO(String name, String coverImage, CustomBeatmapLevelCollectionSO beatmapLevelCollection)
    {
		_playListLocalizedName = name;
		byte[] imageBytes = Convert.FromBase64String(coverImage.Substring(coverImage.IndexOf(",") + 1));
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(imageBytes);
		_coverImage = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
		_beatmapLevelCollection = beatmapLevelCollection;
    }

	public string collectionName
	{
		get
		{
			return Localization.Get(this._playListLocalizedName);
		}
	}

	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	[SerializeField]
	protected string _playListLocalizedName;

	[SerializeField]
	protected Sprite _coverImage;

	[SerializeField]
	protected CustomBeatmapLevelCollectionSO _beatmapLevelCollection;
}
