using Assets._Project.Develop.Runtime.Gameplay.GameModeManagement;
using Assets._Project.Develop.Runtime.Gameplay.GameRules;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.Gameplay.EndGamePopup;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using System;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
	public class GameplayPresentersFactory
	{
		private readonly DIContainer _container;

		public GameplayPresentersFactory(DIContainer container)
		{
			_container = container;
		}

		public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
		{
			return new GameplayScreenPresenter(
				view,
				_container.Resolve<ProjectPresentersFactory>(),
				_container.Resolve<IRule>(),
				_container.Resolve<GameplayPopupService>());
		}

		public EndGamePopupPresenter CreateEndGamePopupPresenter(EndGamePopupView view, string message)
		{
			return new EndGamePopupPresenter(
				view,
				_container.Resolve<ICoroutinesPerformer>(),
				_container.Resolve<GameplayCycle>(),
				message);
		}

		public static implicit operator GameplayPresentersFactory(ProjectPresentersFactory v)
		{
			throw new NotImplementedException();
		}
	}
}
