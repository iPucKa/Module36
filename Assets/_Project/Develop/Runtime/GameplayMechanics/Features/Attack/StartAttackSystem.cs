using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.Attack
{
	public class StartAttackSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent _startAttackRequest;
		private ReactiveEvent _startAttackEvent;
		private ReactiveVariable<bool> _inAttackProcess;

		private ICompositCondition _canStartAttack;

		private IDisposable _attackRequestDispose;

		public void OnInit(Entity entity)
		{
			_startAttackRequest = entity.StartAttackRequest;
			_startAttackEvent = entity.StartAttackEvent;
			_inAttackProcess = entity.InAttackProcess;
			_canStartAttack = entity.CanStartAttack;

			_attackRequestDispose = _startAttackRequest.Subscribe(OnAttackRequest);
		}

		public void OnDispose()
		{
			_attackRequestDispose.Dispose();
		}

		private void OnAttackRequest()
		{
			if (_canStartAttack.Evaluate())
			{
				_inAttackProcess.Value = true;
				_startAttackEvent.Invoke();
				Debug.Log("Старт атаки");
			}
			else
			{
				Debug.Log("Не могу атаковать");
			}
		}
	}
}
