using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.Attack.Area
{
	public class AreaAttackSystem : IInitializableSystem, IDisposableSystem
	{
		private Buffer<Entity> _contacts;
		private ReactiveVariable<float> _damage;
		private ReactiveEvent _attackDelayEndEvent;

		private List<Entity> _processedEntities;

		private IDisposable _attackDelayEndDisposable;


		public void OnInit(Entity entity)
		{
			_contacts = entity.AreaContactEntitiesBuffer;
			_damage = entity.AreaContactDamage;
			_attackDelayEndEvent = entity.AttackDelayEndEvent;

			_processedEntities = new List<Entity>(_contacts.Items.Length);

			_attackDelayEndDisposable = _attackDelayEndEvent.Subscribe(OnAttackDelayEnd);
		}

		public void OnDispose()
		{
			_attackDelayEndDisposable.Dispose();
		}

		private void OnAttackDelayEnd()
		{
			Debug.Log("Ищу кому бы нанести дамаг, число контактов: " + _contacts.Count);

			//обработка первого касания
			for (int i = 0; i < _contacts.Count; i++)
			{
				Entity contactEntity = _contacts.Items[i];

				if (_processedEntities.Contains(contactEntity) == false)
				{
					_processedEntities.Add(contactEntity);

					if (contactEntity.HasComponent<TakeDamageRequest>())
						contactEntity.TakeDamageRequest.Invoke(_damage.Value);
				}
			}

			//обработка выхода из касания
			for (int i = _processedEntities.Count - 1; i >= 0; i--)
				if (ContainInContacts(_processedEntities[i]) == false)
					_processedEntities.Remove(_processedEntities[i]);
		}

		private bool ContainInContacts(Entity entity)
		{
			for (int i = 0; i < _contacts.Count; i++)
				if (_contacts.Items[i] == entity)
					return true;

			return false;
		}
	}
}
