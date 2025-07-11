using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
	[CreateAssetMenu(menuName = "Configs/Gameplay/GameplayConfig", fileName = "GameplayConfig")]
	public class GameplayConfig : ScriptableObject
	{
		[field: SerializeField] public int ValueToResetProgress { get; private set; }
		[field: SerializeField] public int WinValue { get; private set; }
		[field: SerializeField] public int DefeatValue { get; private set; }

		public int GetWinValue => WinValue;

		public int GetDefeatValue => DefeatValue;
	}
}
