using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.ApplyDamage
{
	public class TakeDamageRequest : IEntityComponent
	{
		public ReactiveEvent<float> Value;
	}

	public class TakeDamageEvent : IEntityComponent
	{
		public ReactiveEvent<float> Value;
	}

	public class CanApplyDamage : IEntityComponent
	{
		public ICompositCondition Value;
	}
}
