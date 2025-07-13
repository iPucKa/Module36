using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.ApplyDamage
{
	public class ApplyDamageSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent<float> _damageRequest;
		private ReactiveEvent<float> _damageEvent;

		private ReactiveVariable<float> _health;
		private ICompositCondition _canApplyDamage;

		private IDisposable _requestDisposable;

		public void OnInit(Entity entity)
		{
			_damageRequest = entity.TakeDamageRequest;
			_damageEvent = entity.TakeDamageEvent;
			_health = entity.CurrentHealth;
			_canApplyDamage = entity.CanApplyDamage;

			_requestDisposable = _damageRequest.Subscribe(OnDamageRequest);
		}

		public void OnDispose()
		{
			_requestDisposable.Dispose();
		}

		private void OnDamageRequest(float damage)
		{
			if (damage < 0)
				throw new ArgumentOutOfRangeException(nameof(damage));

			if (_canApplyDamage.Evaluate() == false)
				return;

			_health.Value = MathF.Max(_health.Value - damage, 0);
			_damageEvent.Invoke(damage);

			Debug.Log("Я получил урон!");
		}
	}
}
