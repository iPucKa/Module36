using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.GameplayMechanics
{
	[CreateAssetMenu(menuName = "Configs/GameplayMechanics/NewGameplayMechanicsConfig", fileName = "GameplayMechanicsConfig")]
	public class GameplayMechanicsConfig : ScriptableObject
	{
		[field: SerializeField] public float MoveSpeed { get; private set; }
		[field: SerializeField] public float RotationSpeed { get; private set; }
	}
}
