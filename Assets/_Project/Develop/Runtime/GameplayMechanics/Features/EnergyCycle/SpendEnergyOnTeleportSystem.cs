using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.EnergyCycle
{
	public class SpendEnergyOnTeleportSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent _teleportEvent;
		private ReactiveVariable<float> _energy;
		private ReactiveVariable<float> _energyPrice;

		private IDisposable _teleportEventDisposable;

		public void OnInit(Entity entity)
		{
			_teleportEvent = entity.TeleportingEvent;
			_energy = entity.CurrentEnergy;
			_energyPrice = entity.TeleportByEnergyValue;

			_teleportEventDisposable = _teleportEvent.Subscribe(OnTeleportEvent);
		}

		public void OnDispose()
		{
			_teleportEventDisposable.Dispose();
		}

		private void OnTeleportEvent()
		{
			_energy.Value = MathF.Max(_energy.Value - _energyPrice.Value, 0);
			Debug.Log("Я телепортировался, осталось энергии " + _energy.Value);
		}
	}
}
