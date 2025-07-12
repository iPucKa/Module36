using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature
{
	public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
	{
		private const float _deadZone = 0.1f;

		private ReactiveVariable<Vector3> _direction;
		private ReactiveVariable<float> _rotationSpeed;
		private Rigidbody _rigidbody;

		private ICompositCondition _canRotate;

		public void OnInit(Entity entity)
		{
			_direction = entity.RotationDirection;
			_rotationSpeed = entity.RotationSpeed;
			_rigidbody = entity.Rigidbody;

			_canRotate = entity.CanRotate;

			if (_direction.Value != Vector3.zero)
				_rigidbody.transform.rotation = Quaternion.LookRotation(_direction.Value.normalized);
		}

		public void OnUpdate(float deltaTime)
		{
			ProcessRotateTo(deltaTime);
		}

		private void ProcessRotateTo(float deltaTime)
		{
			if (_canRotate.Evaluate() == false)
				return;

			if (_direction.Value == Vector3.zero)
				return;

			Vector3 direction = _direction.Value.normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			float step = _rotationSpeed.Value * deltaTime;

			Quaternion rotation = Quaternion.RotateTowards(_rigidbody.rotation, lookRotation, step);
			_rigidbody.MoveRotation(rotation);
		}
	}
}
