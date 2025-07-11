using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature
{
	public class RotationSpeed : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class RotationDirection : IEntityComponent
	{
		public ReactiveVariable<Vector3> Value;
	}
}
