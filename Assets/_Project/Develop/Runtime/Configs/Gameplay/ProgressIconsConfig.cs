using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
	[CreateAssetMenu(menuName = "Configs/Gameplay/NewProgressIconsConfig", fileName = "ProgressIconsConfig")]
	public class ProgressIconsConfig : ScriptableObject
	{
		[SerializeField] private List<ProgressConfig> _сonfigs;

		public Sprite GetSpriteFor(GameProgressTypes progressType)
			=> _сonfigs.First(config => config.Type == progressType).Sprite;

		[Serializable]
		private class ProgressConfig
		{
			[field: SerializeField] public GameProgressTypes Type { get; private set; }
			[field: SerializeField] public Sprite Sprite { get; private set; }
		}
	}
}
