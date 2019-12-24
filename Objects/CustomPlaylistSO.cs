using System;
using Polyglot;
using UnityEngine;

public class CustomPlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
{
	public const String DEFAULT_IMAGE = "data:image/png;base64,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";

	public static CustomPlaylistSO CreateInstance(String name, String coverImage, CustomBeatmapLevelCollectionSO beatmapLevelCollection)
	{
		CustomPlaylistSO customPlaylistSO = ScriptableObject.CreateInstance<CustomPlaylistSO>();
		customPlaylistSO._playListLocalizedName = name;
		byte[] imageBytes;
		try
		{
			imageBytes = Convert.FromBase64String(coverImage.Substring(coverImage.IndexOf(",") + 1));
		}
		catch (Exception e)
		{
			imageBytes = Convert.FromBase64String(DEFAULT_IMAGE.Substring(DEFAULT_IMAGE.IndexOf(",") + 1));
		}
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(imageBytes);
		customPlaylistSO._coverImage = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
		customPlaylistSO._beatmapLevelCollection = beatmapLevelCollection;
		return customPlaylistSO;
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
