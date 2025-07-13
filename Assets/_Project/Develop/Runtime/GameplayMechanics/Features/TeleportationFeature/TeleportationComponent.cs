using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.TeleportationFeature
{
	public class TeleportPosition : IEntityComponent
	{
		public ReactiveVariable<Vector3> Value;
	}	

	public class TeleportRadius : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class CanTeleport : IEntityComponent
	{
		public ICompositCondition Value;
	}

	public class TeleportingRequest : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class TeleportingEvent : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class TeleportCooldownInitialTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class TeleportCooldownCurrentTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class InTeleportCooldown : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
	}
}
