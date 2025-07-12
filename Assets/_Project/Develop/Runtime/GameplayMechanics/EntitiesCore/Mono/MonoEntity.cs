using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono
{
	public class MonoEntity : MonoBehaviour
	{
		private CollidersRegistryService _colllidersRegistryService;
		private Entity _linkedEntity;

		public Entity LinkedEntity => _linkedEntity;

		public void Initialize(CollidersRegistryService colllidersRegistryService)
		{
			_colllidersRegistryService = colllidersRegistryService;
		}

		public void Link(Entity entity)
		{
			_linkedEntity = entity;

			MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

			if (registrators != null)
				foreach (MonoEntityRegistrator registrator in registrators)
					registrator.Register(entity);

			foreach (Collider collider in GetComponentsInChildren<Collider>())
				_colllidersRegistryService.Register(collider, entity);
		}

		public void Cleanup(Entity entity)
		{
			foreach (Collider collider in GetComponentsInChildren<Collider>())
				_colllidersRegistryService.Unregister(collider);

			_linkedEntity = null;
		}
	}
}
