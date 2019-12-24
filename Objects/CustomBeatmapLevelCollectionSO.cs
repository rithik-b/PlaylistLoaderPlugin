using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class CustomBeatmapLevelCollectionSO : PersistentScriptableObject, IBeatmapLevelCollection
{
	public static CustomBeatmapLevelCollectionSO CreateInstance(IPreviewBeatmapLevel[] beatmapLevels)
	{
		CustomBeatmapLevelCollectionSO customBeatmapLevelCollectionSO = PersistentScriptableObject.CreateInstance<CustomBeatmapLevelCollectionSO>();
		customBeatmapLevelCollectionSO._beatmapLevels = beatmapLevels;
		return customBeatmapLevelCollectionSO;
	}

	public IPreviewBeatmapLevel[] beatmapLevels
	{
		get
		{
			return this._beatmapLevels;
		}
	}

	[SerializeField]
	protected IPreviewBeatmapLevel[] _beatmapLevels;
}
