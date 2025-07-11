using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Develop.Runtime.Gameplay.Progress
{
	public class ProgressService : IDataReader<PlayerData>, IDataWriter<PlayerData>
	{
		private readonly Dictionary<GameProgressTypes, ReactiveVariable<int>> _achievements;		

		public ProgressService(
			Dictionary<GameProgressTypes, ReactiveVariable<int>> achievements,
			PlayerDataProvider playerDataProvider)
		{			
			_achievements = new Dictionary<GameProgressTypes, ReactiveVariable<int>>(achievements);
			
			playerDataProvider.RegisterWriter(this);
			playerDataProvider.RegisterReader(this);
		}

		public List<GameProgressTypes> AllKindOfProgress => _achievements.Keys.ToList();

		public IReadOnlyVariable<int> GetProgress(GameProgressTypes type) => _achievements[type];

		public void ReadFrom(PlayerData data)
		{
			foreach (KeyValuePair<GameProgressTypes, int> achievement in data.ProgressData)
			{
				if (_achievements.ContainsKey(achievement.Key))
					_achievements[achievement.Key].Value = achievement.Value;
				else
					_achievements.Add(achievement.Key, new ReactiveVariable<int>(achievement.Value));
			}
		}

		public void WriteTo(PlayerData data)
		{			
			foreach (KeyValuePair<GameProgressTypes, ReactiveVariable<int>> achievement in _achievements)
			{
				if (data.ProgressData.ContainsKey(achievement.Key))
					data.ProgressData[achievement.Key] = achievement.Value.Value;
				else
					data.ProgressData.Add(achievement.Key, achievement.Value.Value);
			}
		}

		public void Increase(GameProgressTypes type) => _achievements[type].Value ++;		

		public void ResetValue(GameProgressTypes type) => _achievements[type].Value = 0;
	}
}
