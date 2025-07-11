using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature
{
	public class CharacterControllerRotationSystem : IInitializableSystem, IUpdatableSystem
	{
		private const float _deadZone = 0.1f;

		private ReactiveVariable<Vector3> _rotationDirection;
		private ReactiveVariable<float> _rotationSpeed;
		private CharacterController _characterController;

		public void OnInit(Entity entity)
		{
			_rotationDirection = entity.RotationDirection;
			_rotationSpeed = entity.RotationSpeed;
			_characterController = entity.CharacterController;
		}

		public void OnUpdate(float deltaTime)
		{
			ProcessRotateTo(deltaTime);
		}
		
		private void ProcessRotateTo(float deltaTime)
		{
			if (_rotationDirection.Value.magnitude <= _deadZone)
				return;

			Vector3 direction = _rotationDirection.Value.normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			float step = _rotationSpeed.Value * deltaTime;

			_characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, lookRotation, step);
		}
	}
}
