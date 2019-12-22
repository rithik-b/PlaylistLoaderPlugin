using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class CustomBeatmapLevelCollectionSO : PersistentScriptableObject, IBeatmapLevelCollection
{
	public CustomBeatmapLevelCollectionSO(IPreviewBeatmapLevel[] beatmapLevels)
	{
		_beatmapLevels = beatmapLevels;
	}

	public IPreviewBeatmapLevel[] beatmapLevels
	{
		get
		{
			return this._beatmapLevels;
		}
	}

	// Token: 0x04000589 RID: 1417
	[SerializeField]
	protected IPreviewBeatmapLevel[] _beatmapLevels;
}
