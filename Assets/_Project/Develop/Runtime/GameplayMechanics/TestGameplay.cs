using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics
{
	public class TestGameplay : MonoBehaviour
	{
		private DIContainer _container;
		private EntitiesFactory _entitiesFactory;

		private Entity _entity;
		//private Entity _entityCC;

		private bool _isRunning;

		public void Initialize(DIContainer container)
		{
			_container = container;
			_entitiesFactory = _container.Resolve<EntitiesFactory>();
		}

		public void Run()
		{
			//_entity = _entitiesFactory.CreateHeroRBEntity(Vector3.zero);
			//_entityCC = _entitiesFactory.CreateHeroCCEntity(new Vector3(0,0,3));

			_entity = _entitiesFactory.CreateTeleportedEntity(Vector3.zero);

			_isRunning = true;
		}

		private void Update()
		{
			if (_isRunning == false)
				return;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				_entity.TeleportingRequest.Invoke();
				//Debug.Log("Текущий уровень здоровья: " + _entity.CurrentHealth.Value.ToString());
			}

			//Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

			//_entity.MoveDirection.Value = input;
			//_entity.RotationDirection.Value = input;

			//_entityCC.MoveDirection.Value = input;
			//_entityCC.RotationDirection.Value = input;
		}
	}
}
