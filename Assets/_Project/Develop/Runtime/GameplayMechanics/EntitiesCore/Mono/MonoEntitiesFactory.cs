using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono
{
	public class MonoEntitiesFactory : IInitializable, IDisposable
	{
		private readonly ResourcesAssetsLoader _resources;

		private readonly EntitiesLifeContext _entitiesLifeContext;

		private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();

		public MonoEntitiesFactory(ResourcesAssetsLoader resources, EntitiesLifeContext entitiesLifeContext)
		{
			_resources = resources;
			_entitiesLifeContext = entitiesLifeContext;
		}

		public MonoEntity Create(Entity entity, Vector3 position, string path)
		{
			MonoEntity prefab = _resources.Load<MonoEntity>(path);
			MonoEntity viewInstance = Object.Instantiate(prefab, position, Quaternion.identity, null);

			viewInstance.Setup(entity);
			_entityToMono.Add(entity, viewInstance);
			return viewInstance;
		}

		public void Initialize()
		{
			_entitiesLifeContext.Released += OnEntityReleased;
		}

		public void Dispose()
		{
			_entitiesLifeContext.Released -= OnEntityReleased;

			foreach (Entity entity in _entityToMono.Keys)
				CleanupFor(entity);

			_entityToMono.Clear();
		}

		private void OnEntityReleased(Entity entity)
		{
			CleanupFor(entity);

			_entityToMono.Remove(entity);
		}

		private void CleanupFor(Entity entity)
		{
			MonoEntity monoEntity = _entityToMono[entity];
			monoEntity.Cleanup(entity);
			Object.Destroy(monoEntity.gameObject);
		}
	}
}
