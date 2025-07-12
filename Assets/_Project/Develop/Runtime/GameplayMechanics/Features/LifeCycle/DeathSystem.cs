using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.LifeCycle
{
	public class DeathSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<bool> _isDead;
		//private ReactiveVariable<float> _currentHealth;
		private ICompositCondition _mustDie;

		public void OnInit(Entity entity)
		{
			_isDead = entity.IsDead;
			_mustDie = entity.MustDie;
		}

		public void OnUpdate(float deltaTime)
		{
			if (_isDead.Value)
				return;

			if (_mustDie.Evaluate())
				_isDead.Value = true;
		}
	}
}
