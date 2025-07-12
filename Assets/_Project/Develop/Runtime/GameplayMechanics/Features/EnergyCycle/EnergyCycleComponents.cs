using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.EnergyCycle
{
	public class CurrentEnergy : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class MaxEnergy : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class TeleportByEnergyValue : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class EnergyRecoveryInitialTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class EnergyRecoveryCurrentTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class InRestoreEnergyProcess : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
	}

	public class CanRestoreEnergy : IEntityComponent
	{
		public ICompositCondition Value;
	}
}
