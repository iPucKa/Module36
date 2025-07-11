using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Progress;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu.GameModeSelectorPopup;
using Assets._Project.Develop.Runtime.UI.Progress;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.UI
{
	public class ProjectPresentersFactory
	{
		private readonly DIContainer _container;

		public ProjectPresentersFactory(DIContainer container)
		{
			_container = container;
		}

		public CurrencyPresenter CreateCurrencyPresenter(
			IconTextView view,
			IReadOnlyVariable<int> currency,
			CurrencyTypes currencyType)
		{
			return new CurrencyPresenter(
				currency,
				currencyType,
				_container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
				view);
		}

		public WalletPresenter CreateWalletPresenter(IconTextListView view)
		{
			return new WalletPresenter(
				_container.Resolve<WalletService>(),
				this,
				_container.Resolve<ViewsFactory>(),
				view);
		}

		public ProgressPresenter CreateProgressPresenter(
			IconTextView view,
			IReadOnlyVariable<int> progressValue,
			GameProgressTypes progressType)
		{
			return new ProgressPresenter(
				progressValue,
				progressType,
				_container.Resolve<ConfigsProviderService>().GetConfig<ProgressIconsConfig>(),
				view);
		}

		public ProgressBarPresenter CreateProgressBarPresenter(IconTextListView view)
		{
			return new ProgressBarPresenter(
				_container.Resolve<ProgressService>(),
				this,
				_container.Resolve<ViewsFactory>(),
				view);
		}

		//public GameModeSelectorPopupPresenter CreateGameModeSelectorPopupPresenter(GameModeSelectorPopupView view)
		//{
		//	return new GameModeSelectorPopupPresenter(
		//		view,
		//		_container.Resolve<ModeService>(),
		//		_container.Resolve<ICoroutinesPerformer>());
		//}

		//public EndGamePopupPresenter CreateEndGamePopupPresenter(EndGamePopupView view)
		//{
		//	return new EndGamePopupPresenter(
		//		view,
		//		_container.Resolve<ICoroutinesPerformer>(),
		//		_container.Resolve<GameplayCycle>());
		//}
	}
}
