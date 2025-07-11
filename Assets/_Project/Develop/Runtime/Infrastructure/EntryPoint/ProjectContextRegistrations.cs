using Assets._Project.Develop.Runtime.Gameplay.Progress;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.Serializers;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
	public class ProjectContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			//Регистрации
			container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
			container.RegisterAsSingle(CreateConfigsProviderService);
			container.RegisterAsSingle(CreateResourcesAssetsLoader);
			container.RegisterAsSingle(CreateSceneLoaderService);
			container.RegisterAsSingle(CreateSceneSwitcherService);
			container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
			container.RegisterAsSingle(CreateWalletService).NonLazy();
			container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);
			container.RegisterAsSingle(CreatePlayerDataProvider);
			container.RegisterAsSingle(CreateProgressService).NonLazy();
			container.RegisterAsSingle(CreateProgressRestoreService);
			container.RegisterAsSingle(CreateViewsFactory);
			container.RegisterAsSingle(CreateProjectPresentersFactory);
		}

		//Способ создания фабрики презентеров
		private static ProjectPresentersFactory CreateProjectPresentersFactory(DIContainer c)
			=> new ProjectPresentersFactory(c);

		//Способ создания фабрики вьюх
		private static ViewsFactory CreateViewsFactory(DIContainer c)
			=> new ViewsFactory(c.Resolve<ResourcesAssetsLoader>());

		//Способ создания сервиса сброса числа побед и поражений
		private static ProgressRestoreService CreateProgressRestoreService(DIContainer c)
		{
			return new ProgressRestoreService(
				c.Resolve<ICoroutinesPerformer>(),
				c.Resolve<PlayerDataProvider>(),
				c.Resolve<ConfigsProviderService>(),
				c.Resolve<WalletService>(),
				c.Resolve<ProgressService>());
		}

		//Способ создания прогресса побед и поражений
		private static ProgressService CreateProgressService(DIContainer c)
		{
			Dictionary<GameProgressTypes, ReactiveVariable<int>> achievements = new Dictionary<GameProgressTypes, ReactiveVariable<int>>();

			foreach (GameProgressTypes progressType in Enum.GetValues(typeof(GameProgressTypes)))
				achievements[progressType] = new ReactiveVariable<int>();

			return new ProgressService(achievements, c.Resolve<PlayerDataProvider>());
		}

		//Способ создания провайдера данных
		private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
			=> new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());

		//Способ создания сохранений и загрузок
		private static SaveLoadService CreateSaveLoadService(DIContainer c)
		{
			IDataSerializer dataSerializer = new JsonSerializer();
			IDataKeysStorage dataKeyStorage = new MapDataKeysStorage();

			string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;
			IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

			return new SaveLoadService(dataSerializer, dataKeyStorage, dataRepository);
		}

		//Способ создания кошелька
		private static WalletService CreateWalletService(DIContainer c)
		{
			Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>();

			foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
				currencies[currencyType] = new ReactiveVariable<int>();

			return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
		}

		//Способ создания
		private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
			=> new SceneSwitcherService(
				c.Resolve<SceneLoaderService>(),
				c.Resolve<ILoadingScreen>(),
				c);

		//Способ создания
		private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
			=> new SceneLoaderService();

		//Способ создания
		private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

		//Способ создания
		private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
		{
			ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

			ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

			return new ConfigsProviderService(resourcesConfigsLoader);
		}

		//Способ создания
		private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
		{
			ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

			CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
				.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

			return Object.Instantiate(coroutinesPerformerPrefab);
		}

		//Способ создания
		private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
		{
			ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

			StandardLoadingScreen standartLoadingScreenPrefab = resourcesAssetsLoader
				.Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

			return Object.Instantiate(standartLoadingScreenPrefab);
		}
	}
}
