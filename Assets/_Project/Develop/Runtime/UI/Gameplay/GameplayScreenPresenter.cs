using Assets._Project.Develop.Runtime.Gameplay.GameRules;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Progress;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
	public class GameplayScreenPresenter : IPresenter
	{
		private const string WinMessage = "YOU WIN";
		private const string DefeatMessage = "TRY AGAIN";

		private readonly GameplayScreenView _screen;
		private readonly ProjectPresentersFactory _projectPresentersFactory;
		private readonly IRule _rule;
		private readonly GameplayPopupService _popupService;

		private readonly List<IPresenter> _childPresenters = new();

		public GameplayScreenPresenter(
			GameplayScreenView screen,
			ProjectPresentersFactory projectPresentersFactory,
			IRule rule,
			GameplayPopupService popupService)
		{
			_screen = screen;
			_projectPresentersFactory = projectPresentersFactory;
			_rule = rule;
			_popupService = popupService;
		}

		public void Initialize()
		{
			_screen.IsTyped += OnPlayerTyped;
			_rule.IsGenerated += OnGenerated;
			_rule.IsMatch += OnGameWin;
			_rule.IsNotMatch += OnGameDefeat;

			CreateWallet();

			CreateProgressBar();

			foreach (IPresenter presenter in _childPresenters)
				presenter.Initialize();
		}

		public void Dispose()
		{
			_screen.IsTyped -= OnPlayerTyped;
			_rule.IsGenerated -= OnGenerated;
			_rule.IsMatch += OnGameWin;
			_rule.IsNotMatch += OnGameDefeat;

			foreach (IPresenter presenter in _childPresenters)
				presenter.Dispose();

			_childPresenters.Clear();
		}

		private void CreateWallet()
		{
			WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);

			_childPresenters.Add(walletPresenter);
		}

		private void CreateProgressBar()
		{
			ProgressBarPresenter progressPresenter = _projectPresentersFactory.CreateProgressBarPresenter(_screen.ProgressView);

			_childPresenters.Add(progressPresenter);
		}

		private void OnGameWin() => _popupService.OpenEndGamePopup(WinMessage);
		
		private void OnGameDefeat() => _popupService.OpenEndGamePopup(DefeatMessage);		

		private void OnGenerated(string generatedText) => _screen.SetText(generatedText);		

		private void OnPlayerTyped(string playerInput) => _rule.Check(playerInput);		
	}
}
