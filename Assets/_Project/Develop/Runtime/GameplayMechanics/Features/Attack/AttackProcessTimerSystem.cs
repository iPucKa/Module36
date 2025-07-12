using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.Attack
{
	public class AttackProcessTimerSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
	{
		private ReactiveVariable<float> _currentTime;
		private ReactiveVariable<bool> _inAttackProcess;
		private ReactiveEvent _startAttackEvent;

		private IDisposable _startAttackEventDisposable;

		public void OnInit(Entity entity)
		{
			_currentTime = entity.AttackProcessCurrentTime;
			_inAttackProcess = entity.InAttackProcess;
			_startAttackEvent = entity.StartAttackEvent;

			_startAttackEventDisposable = _startAttackEvent.Subscribe(OnStartAttackProcess);
		}

		public void OnUpdate(float deltaTime)
		{
			if (_inAttackProcess.Value == false)
				return;

			_currentTime.Value += deltaTime;
		}

		public void OnDispose()
		{
			_startAttackEventDisposable.Dispose();
		}

		private void OnStartAttackProcess()
		{
			_currentTime.Value = 0;
		}
	}
}
