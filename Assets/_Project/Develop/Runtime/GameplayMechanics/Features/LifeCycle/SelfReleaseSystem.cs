using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.LifeCycle
{
	public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
	{
		private readonly EntitiesLifeContext _entitiesLifeContext;

		private Entity _entity;
		//private ReactiveVariable<bool> _isDead;
		//private ReactiveVariable<bool> _inDeathProcess;
		private ICompositCondition _mustSelfRelease;

		public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
		{
			_entitiesLifeContext = entitiesLifeContext;
		}

		public void OnInit(Entity entity)
		{
			_entity = entity;
			//_isDead = _entity.IsDead;
			//_inDeathProcess = _entity.InDeathProcess;
			_mustSelfRelease = entity.MustSelfRelease;
		}

		public void OnUpdate(float deltaTime)
		{
			//if(_isDead.Value && _inDeathProcess.Value == false)
			if (_mustSelfRelease.Evaluate())
				_entitiesLifeContext.Release(_entity);
		}
	}
}
