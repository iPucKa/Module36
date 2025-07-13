using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.TeleportationFeature
{
	public class TeleportCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
	{
		private ReactiveVariable<float> _currentTime;
		private ReactiveVariable<float> _initialTime;
		private ReactiveVariable<bool> _inTeleportCoolDown;

		private ReactiveEvent _endTeleportEvent;
		private IDisposable _endTeleportEventDisposable;

		public void OnInit(Entity entity)
		{
			_currentTime = entity.TeleportCooldownCurrentTime;
			_initialTime = entity.TeleportCooldownInitialTime;
			_inTeleportCoolDown = entity.InTeleportCooldown;
			_endTeleportEvent = entity.TeleportingEvent;

			_endTeleportEventDisposable = _endTeleportEvent.Subscribe(OnEndTeleport);
		}

		public void OnUpdate(float deltaTime)
		{
			if (_inTeleportCoolDown.Value == false)
				return;

			_currentTime.Value -= deltaTime;

			if (CooldownIsOver())
			{
				_inTeleportCoolDown.Value = false;
				Debug.Log("КУЛДАУН ЗАКОНЧИЛСЯ");
			}
		}

		public void OnDispose()
		{
			_endTeleportEventDisposable.Dispose();
		}

		private void OnEndTeleport()
		{
			Debug.Log("КУЛДАУН НАЧАЛСЯ");
			_currentTime.Value = _initialTime.Value;
			_inTeleportCoolDown.Value = true;
		}

		private bool CooldownIsOver() => _currentTime.Value <= 0;
	}
}
