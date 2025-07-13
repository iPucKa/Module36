using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.EnergyCycle
{
	public class RestoreEnergySystem : IInitializableSystem, IUpdatableSystem
	{
		private const float EnergyToRestore = 0.1f;

		private ReactiveVariable<float> _currentEnergy;
		private ReactiveVariable<float> _maxEnergy;

		private ReactiveVariable<float> _initialTime;
		private ReactiveVariable<float> _curentTime;

		private ICompositCondition _canRestoreEnergy;

		private float _energyToRestore;

		public void OnInit(Entity entity)
		{
			_maxEnergy = entity.MaxEnergy;
			_currentEnergy = entity.CurrentEnergy;

			_initialTime = entity.EnergyRecoveryInitialTime;
			_curentTime = entity.EnergyRecoveryCurrentTime;

			_canRestoreEnergy = entity.CanRestoreEnergy;

			_energyToRestore = _maxEnergy.Value * EnergyToRestore;
			_curentTime.Value = _initialTime.Value;
		}

		public void OnUpdate(float deltaTime)
		{
			if (_canRestoreEnergy.Evaluate() == false)
				return;

			_curentTime.Value -= deltaTime;

			if (MustRestore())
			{
				_currentEnergy.Value = MathF.Min(_currentEnergy.Value + _energyToRestore, _maxEnergy.Value);

				Debug.Log("Восстанавливаю энергию, текущее значение: " + _currentEnergy.Value);

				_curentTime.Value = _initialTime.Value;
			}
		}

		private bool MustRestore() => _curentTime.Value <= 0;
	}
}
