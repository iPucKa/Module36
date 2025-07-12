using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.LifeCycle
{
	public class DeathProcessTimerSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
	{
		private ReactiveVariable<bool> _isDead;
		private ReactiveVariable<bool> _inDeathProcess;
		private ReactiveVariable<float> _initialTime;
		private ReactiveVariable<float> _curentTime;

		private IDisposable _isDeadChangedDisposable;

		public void OnInit(Entity entity)
		{
			_isDead = entity.IsDead;
			_inDeathProcess = entity.InDeathProcess;
			_initialTime = entity.DeathProcessInitialTime;
			_curentTime = entity.DeathProcessCurrentTime;

			_isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
		}
		public void OnUpdate(float deltaTime)
		{
			if (_inDeathProcess.Value == false)
				return;

			_curentTime.Value -= deltaTime;

			if (CooldownIsOver())
				_inDeathProcess.Value = false;
		}

		public void OnDispose()
		{
			_isDeadChangedDisposable.Dispose();
		}

		private void OnIsDeadChanged(bool arg1, bool isDead)
		{
			if (isDead)
			{
				_curentTime.Value = _initialTime.Value;
				_inDeathProcess.Value = true;
			}
		}

		private bool CooldownIsOver() => _curentTime.Value <= 0;
	}
}
