using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics
{
	public class TestGameplay : MonoBehaviour
	{
		private DIContainer _container;
		private EntitiesFactory _entitiesFactory;

		private Entity _entityHero;

		private bool _isRunning;

		public void Initialize(DIContainer container)
		{
			_container = container;
			_entitiesFactory = _container.Resolve<EntitiesFactory>();
		}

		public void Run()
		{
			Entity entityGhost1 = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 3);
			Entity entityGhost2 = _entitiesFactory.CreateGhost(Vector3.zero - Vector3.forward * 3);
			Entity entityGhost3 = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.right * 3);
			//_entityCC = _entitiesFactory.CreateHeroCCEntity(new Vector3(0,0,3));

			_entityHero = _entitiesFactory.CreateHero(Vector3.zero);

			_isRunning = true;
		}

		private void Update()
		{
			if (_isRunning == false)
				return;

			if (Input.GetKeyDown(KeyCode.Space))			
				_entityHero.TeleportingRequest.Invoke();			

			if(Input.GetKeyDown(KeyCode.F))
				_entityHero.TakeDamageRequest.Invoke(50);
		}
	}
}
