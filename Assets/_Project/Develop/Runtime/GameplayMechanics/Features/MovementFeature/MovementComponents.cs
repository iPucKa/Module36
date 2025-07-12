using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature
{
	public class MoveDirection : IEntityComponent
	{
		public ReactiveVariable<Vector3> Value;
	}

	public class MoveSpeed : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class IsMoving : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
	}

	public class CanMove : IEntityComponent
	{
		public ICompositCondition Value;
	}
}
