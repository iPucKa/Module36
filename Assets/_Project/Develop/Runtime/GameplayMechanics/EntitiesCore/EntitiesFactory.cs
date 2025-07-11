using Assets._Project.Develop.Runtime.Configs.GameplayMechanics;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore
{
	public class EntitiesFactory
	{
		private readonly DIContainer _container;
		private readonly ConfigsProviderService _configProviderService;
		private readonly EntitiesLifeContext _entitiesLifeContext;

		private readonly MonoEntitiesFactory _monoEntitiesFactory;

		public EntitiesFactory(DIContainer container)
		{
			_container = container;
			_configProviderService = _container.Resolve<ConfigsProviderService>();
			_entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
			_monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
		}

		public Entity CreateHeroRBEntity(Vector3 position)
		{
			Entity entity = CreateEmpty();

			GameplayMechanicsConfig config = _configProviderService.GetConfig<GameplayMechanicsConfig>();

			_monoEntitiesFactory.Create(entity, position, "Entities/HeroRBEntity");

			entity
				.AddMoveDirection()
				.AddMoveSpeed(new ReactiveVariable<float>(config.MoveSpeed))
				.AddRotationDirection()
				.AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed));

			entity
				.AddSystem(new RigidbodyMovementSystem())
				.AddSystem(new RigidbodyRotationSystem());

			_entitiesLifeContext.Add(entity);

			return entity;
		}

		public Entity CreateHeroCCEntity(Vector3 position)
		{
			Entity entity = CreateEmpty();

			GameplayMechanicsConfig config = _configProviderService.GetConfig<GameplayMechanicsConfig>();

			_monoEntitiesFactory.Create(entity, position, "Entities/HeroCCEntity");

			entity
				.AddMoveDirection()
				.AddMoveSpeed(new ReactiveVariable<float>(config.MoveSpeed))
				.AddRotationDirection()
				.AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed));

			entity
				.AddSystem(new CharacterControllerMovementSystem())
				.AddSystem(new CharacterControllerRotationSystem());

			_entitiesLifeContext.Add(entity);

			return entity;
		}

		private Entity CreateEmpty() => new Entity();
	}
}
