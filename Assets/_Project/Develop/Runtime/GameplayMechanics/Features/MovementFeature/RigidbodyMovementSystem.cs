using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature
{
	public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<Vector3> _moveDirection;
		private ReactiveVariable<float> _moveSpeed;
		private Rigidbody _rigidbody;
		private ReactiveVariable<bool> _isMoving;

		private ICompositCondition _canMove;

		public void OnInit(Entity entity)
		{
			_moveDirection = entity.MoveDirection;
			_moveSpeed = entity.MoveSpeed;
			_rigidbody = entity.Rigidbody;
			_isMoving = entity.IsMoving;

			_canMove = entity.CanMove;
		}

		public void OnUpdate(float deltaTime)
		{
			if (_canMove.Evaluate() == false)
			{
				_rigidbody.velocity = Vector3.zero;
				return;
			}

			Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;

			_isMoving.Value = velocity.magnitude > 0;

			_rigidbody.velocity = velocity;
		}			
	}
}
