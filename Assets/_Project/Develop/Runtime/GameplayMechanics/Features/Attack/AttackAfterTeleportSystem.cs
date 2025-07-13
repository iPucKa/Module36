using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.Attack
{
	public class AttackAfterTeleportSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent _teleported;
		private ReactiveEvent _startAttackReqest;

		private ICompositCondition _canAttack;
		private IDisposable _teleportDispose;

		public void OnInit(Entity entity)
		{
			_teleported = entity.TeleportingEvent;
			_startAttackReqest = entity.StartAttackRequest;
			_canAttack = entity.CanStartAttack;

			_teleportDispose = _teleported.Subscribe(OnTeleported);
		}

		public void OnDispose()
		{
			_teleportDispose.Dispose();
		}

		private void OnTeleported()
		{
			if (_canAttack.Evaluate() == false)
				return;

			_startAttackReqest.Invoke();
		}
	}
}
