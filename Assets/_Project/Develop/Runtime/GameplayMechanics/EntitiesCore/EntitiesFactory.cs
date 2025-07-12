using Assets._Project.Develop.Runtime.Configs.GameplayMechanics;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.Attack;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.EnergyCycle;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.LifeCycle;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.Sensors;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.TeleportationFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore
{
	public class EntitiesFactory
	{
		private readonly DIContainer _container;
		private readonly ConfigsProviderService _configProviderService;
		private readonly EntitiesLifeContext _entitiesLifeContext;
		private readonly CollidersRegistryService _collidersRegistryService;
		private readonly MonoEntitiesFactory _monoEntitiesFactory;

		public EntitiesFactory(DIContainer container)
		{
			_container = container;
			_configProviderService = _container.Resolve<ConfigsProviderService>();
			_entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
			_monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
			_collidersRegistryService = _container.Resolve<CollidersRegistryService>();
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

		public Entity CreateTeleportedEntity(Vector3 position)
		{
			Entity entity = CreateEmpty();

			TeleportedEntityConfig config = _configProviderService.GetConfig<TeleportedEntityConfig>();

			_monoEntitiesFactory.Create(entity, position, "Entities/TeleportedEntity");

			entity
				.AddTeleportRadius(new ReactiveVariable<float>(config.TeleportRadius))
				.AddTeleportingRequest()
				.AddTeleportingEvent()
				.AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
				.AddCurrentHealth(new ReactiveVariable<float>(config.MaxHealth))
				.AddMaxEnergy(new ReactiveVariable<float>(config.MaxEnergy))
				.AddCurrentEnergy(new ReactiveVariable<float>(config.MaxEnergy))
				.AddTeleportByEnergyValue(new ReactiveVariable<float>(config.EnergyValueForTeleport))
				.AddEnergyRecoveryInitialTime(new ReactiveVariable<float>(config.EnergyRecoveryTime))
				.AddEnergyRecoveryCurrentTime()
				.AddIsDead()
				.AddInDeathProcess()
				.AddDeathProcessInitialTime(new ReactiveVariable<float>(config.DeathProcessInitialTime))
				.AddDeathProcessCurrentTime()
				.AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
				.AddContactCollidersBuffer(new Buffer<Collider>(64))
				.AddContactEntitiesBuffer(new Buffer<Entity>(64))
				.AddAttackProcessInitialTime(new ReactiveVariable<float>(3))
				.AddAttackProcessCurrentTime()
				.AddInAttackProcess()
				.AddStartAttackRequest()
				.AddStartAttackEvent()
				.AddEndAttackEvent()
				.AddAttackDelayTime(new ReactiveVariable<float>(config.AttackDelayTime))
				.AddAttackDelayEndEvent()
				.AddInstantAttackDamage(new ReactiveVariable<float>(config.Damage));

			ICompositCondition canTeleport = new CompositCondition()
				.Add(new FuncCondition(() => entity.IsDead.Value == false))
				.Add(new FuncCondition(() => entity.CurrentEnergy.Value >= entity.TeleportByEnergyValue.Value));

			ICompositCondition canRestoreEnergy = new CompositCondition()
				.Add(new FuncCondition(() => entity.IsDead.Value == false))
				.Add(new FuncCondition(() => entity.CurrentEnergy.Value < entity.MaxEnergy.Value));

			ICompositCondition mustDie = new CompositCondition()
				.Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

			ICompositCondition mustSelfRelease = new CompositCondition()
				.Add(new FuncCondition(() => entity.IsDead.Value))
				.Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

			ICompositCondition canApplyDamage = new CompositCondition()
				.Add(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositCondition canStartAttack = new CompositCondition()
				.Add(new FuncCondition(() => entity.IsDead.Value == false))
				.Add(new FuncCondition(() => entity.InAttackProcess.Value == false));

			entity
				.AddCanTeleport(canTeleport)
				.AddCanRestoreEnergy(canRestoreEnergy)
				.AddMustDie(mustDie)
				.AddMustSelfRelease(mustSelfRelease)
				.AddCanStartAttack(canStartAttack);
				//.AddCanApplyDamage(canApplyDamage);


			entity
				.AddSystem(new RigidbodyTeleportingSystem())
				.AddSystem(new SpendEnergySystem())
				.AddSystem(new RestoreEnergySystem())				
				.AddSystem(new BodyContactsDetectingSystem())
				.AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
				//.AddSystem(new ApplyDamageSystem())
				.AddSystem(new StartAttackSystem())
				.AddSystem(new AttackProcessTimerSystem())
				.AddSystem(new AttackDelayEndTriggerSystem())
				.AddSystem(new EndAttackSystem())
				.AddSystem(new DeathSystem())
				.AddSystem(new DisableCollidersOnDeathSystem())
				.AddSystem(new DeathProcessTimerSystem())
				.AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

			_entitiesLifeContext.Add(entity);

			return entity;
		}

		private Entity CreateEmpty() => new Entity();
	}
}
