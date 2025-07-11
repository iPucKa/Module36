using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Infrastructure
{
	public class GameplayMechanicsContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			Debug.Log("Процесс регистрации сервисов на сцене геймплейных механик");

			container.RegisterAsSingle(CreateEntitiesFactory);
			container.RegisterAsSingle(CreateEntitiesLifeContext);
			container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();
		}

		//Способ создания фабрики прослоек между сущностью и Unity
		private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
		{
			return new MonoEntitiesFactory(
				c.Resolve<ResourcesAssetsLoader>(),
				c.Resolve<EntitiesLifeContext>());
		}

		//Способ создания Сервиса жизненного цикла сущностей
		private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
		{
			return new EntitiesLifeContext();
		}

		//Способ создания фабрики сущностей
		private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
		{
			return new EntitiesFactory(c);
		}
	}
}
