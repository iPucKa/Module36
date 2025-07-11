using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
	[CreateAssetMenu(menuName = "Configs/Gameplay/NewStartProgressConfig", fileName = "StartProgressConfig")]
	public class StartProgressConfig : ScriptableObject
	{
		[SerializeField] private List<ProgressConfig> _values;

		public int GetValueFor(GameProgressTypes progressType)
			=> _values.First(config => config.Type == progressType).Value;

		[Serializable]
		private class ProgressConfig
		{
			[field: SerializeField] public GameProgressTypes Type { get; private set; }
			[field: SerializeField] public int Value { get; private set; }
		}
	}
}
