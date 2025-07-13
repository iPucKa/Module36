using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.GameplayMechanics
{
	[CreateAssetMenu(menuName = "Configs/GameplayMechanics/NewTeleportedEntityConfig", fileName = "TeleportedEntityConfig")]
	public class TeleportedEntityConfig : ScriptableObject
	{
		[field: SerializeField] public float MaxHealth { get; private set; }
		[field: SerializeField] public float MaxEnergy { get; private set; }
		[field: SerializeField] public float TeleportRadius { get; private set; }
		[field: SerializeField] public float TeleportCooldown{ get; private set; }
		[field: SerializeField] public float EnergyValueForTeleport { get; private set; }
		[field: SerializeField] public float EnergyRecoveryTime { get; private set; }
		[field: SerializeField] public float DamageRadius { get; private set; }
		[field: SerializeField] public float Damage { get; private set; }
		[field: SerializeField] public float AttackDelayTime { get; private set; }
		[field: SerializeField] public float DeathProcessInitialTime { get; private set; }
		[field: SerializeField] public LayerMask LayerMask { get; private set; }
	}
}
