using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.TeleportationFeature
{
	public class RigidbodyTeleportingSystem : IInitializableSystem, IDisposableSystem
	{
		//private ReactiveVariable<Vector3> _teleportPosition;
		private ReactiveVariable<float> _teleportRadius;
		private ReactiveEvent _teleportRequest;
		private ReactiveEvent _teleportEvent;
		private Rigidbody _rigidbody;
		

		private ICompositCondition _canTeleport;
		private IDisposable _teleportRequestDisposable;


		public void OnInit(Entity entity)
		{
			//_teleportPosition = entity.TeleportPosition;
			_teleportRadius = entity.TeleportRadius;
			_rigidbody = entity.Rigidbody;
			_canTeleport = entity.CanTeleport;
			_teleportRequest = entity.TeleportingRequest;
			_teleportEvent = entity.TeleportingEvent;

			_teleportRequestDisposable = _teleportRequest.Subscribe(OnTeleportRequest);
		}

		private void OnTeleportRequest()
		{
			if (_canTeleport.Evaluate() == false)
			{
				Debug.Log("Не могу телепортироваться!");
				return; 
			}

			Vector3 position = GetRandomPosition();

			_rigidbody.position = position;
			_teleportEvent.Invoke();
		}		

		public void OnDispose()
		{
			_teleportRequestDisposable.Dispose();
		}

		private Vector3 GetRandomPosition()
		{
			Vector2 point = Random.insideUnitCircle * _teleportRadius.Value;

			return new Vector3(point.x, 0f, point.y);
		}
	}
}
