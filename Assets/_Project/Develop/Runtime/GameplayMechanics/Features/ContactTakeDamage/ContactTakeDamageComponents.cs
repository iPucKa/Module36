using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.ContactTakeDamage
{
	public class BodyContactDamage : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class AreaContactDamage : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class AreaContactRadius : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}
}
