using Assets._Project.Develop.Runtime.Gameplay.GameModeManagement;
using Assets._Project.Develop.Runtime.Gameplay.GameRules;
using Assets._Project.Develop.Runtime.Gameplay.Progress;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayContextRegistrations
	{
		private static GameplayInputArgs _inputArgs;

		public static void Process(DIContainer container, GameplayInputArgs args)
		{
			Debug.Log("Процесс регистрации сервисов на сцене геймплея");
			_inputArgs = args;

			container.RegisterAsSingle(CreateGameplayPresentersFactory);
			container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
			container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
			container.RegisterAsSingle(CreateGameplayCycle);
			container.RegisterAsSingle<IRule>(CreateRule).NonLazy();
			container.RegisterAsSingle(CreateGameplayPopupService);
		}

		//Способ создания сервиса попапов геймплея
		private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
		{
			return new GameplayPopupService(
				c.Resolve<ViewsFactory>(),
				c.Resolve<GameplayPresentersFactory>(),
				c.Resolve<GameplayUIRoot>());
		}

		//Способ создания правил игры
		private static MatchSymbolsRule CreateRule(DIContainer c)
			=> new MatchSymbolsRule(c.Resolve<ConfigsProviderService>(), _inputArgs);

		//Способ создания геймплейного цикла
		private static GameplayCycle CreateGameplayCycle(DIContainer c)
			=> new GameplayCycle(
				c.Resolve<WalletService>(),
				c.Resolve<ProgressService>(),
				c.Resolve<PlayerDataProvider>(),
				c.Resolve<ConfigsProviderService>(),
				c.Resolve<SceneSwitcherService>(),
				c.Resolve<ICoroutinesPerformer>(),
				c.Resolve<IRule>(),
				_inputArgs);

		//Способ создания холста для геймплея
		private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
		{
			ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

			GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader
				.Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

			return Object.Instantiate(gameplayUIRootPrefab);
		}

		//Способ создания фабрики всех презентеров геймплея
		private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
			=> new GameplayPresentersFactory(c);

		//Способ создания презентера экрана геймплея
		private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
		{
			GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

			GameplayScreenView view = c
				.Resolve<ViewsFactory>()
				.Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

			GameplayScreenPresenter presenter = c
				.Resolve<GameplayPresentersFactory>()
				.CreateGameplayScreenPresenter(view);

			return presenter;
		}
	}
}
